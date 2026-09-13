using Kopator.Core.Services;

namespace Kopator.Core.Tests;

public class CollectServiceTests
{
    private readonly CollectService _service = new();

    [Fact]
    public void FlattensTreeAndRemovesSubdirectories()
    {
        using var root = new TempDirectory();
        root.WriteFile("top.txt");
        root.WriteFile(Path.Combine("a", "one.txt"));
        root.WriteFile(Path.Combine("a", "b", "two.txt"));

        var result = _service.Execute(new CollectRequest(root.Path, string.Empty), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Completed, result.Outcome);
        Assert.Equal(["one.txt", "top.txt", "two.txt"], root.FileNamesInRoot());
        Assert.Empty(Directory.GetDirectories(root.Path));
    }

    [Fact]
    public void FilesAlreadyInRootAreNotTruncated()
    {
        using var root = new TempDirectory();
        root.WriteFile("top.txt", "keep-me");

        _service.Execute(new CollectRequest(root.Path, string.Empty), null, TestContext.Current.CancellationToken);

        Assert.Equal("keep-me", File.ReadAllText(root.Combine("top.txt")));
    }

    /// <summary>
    /// Ignored files are discarded together with their directory. This is deliberate:
    /// an ignore pattern marks a file as not worth carrying up, and the directory sweep
    /// that ends a collect run takes it with the rest.
    /// </summary>
    [Fact]
    public void IgnoredFilesAreDeletedWithTheirDirectory()
    {
        using var root = new TempDirectory();
        root.WriteFile(Path.Combine("sub", "discard.tmp"), "not kept");
        root.WriteFile(Path.Combine("sub", "move.txt"));

        var result = _service.Execute(new CollectRequest(root.Path, "*.tmp"), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Completed, result.Outcome);
        Assert.Equal(["move.txt"], root.FileNamesInRoot());
        Assert.False(Directory.Exists(root.Combine("sub")));
    }

    [Fact]
    public void SeveralIgnorePatternsAreHonoured()
    {
        using var root = new TempDirectory();
        root.WriteFile(Path.Combine("sub", "a.tmp"));
        root.WriteFile(Path.Combine("sub", "b.bak"));
        root.WriteFile(Path.Combine("sub", "c.txt"));

        _service.Execute(new CollectRequest(root.Path, "*.tmp, *.bak"), null, TestContext.Current.CancellationToken);

        Assert.Equal(["c.txt"], root.FileNamesInRoot());
        Assert.Empty(Directory.GetDirectories(root.Path));
    }

    /// <summary>
    /// A file matching an ignore pattern directly in the root is not moved anywhere and
    /// is not swept away either - only subdirectories are removed.
    /// </summary>
    [Fact]
    public void IgnoredFilesInTheRootSurvive()
    {
        using var root = new TempDirectory();
        root.WriteFile("keep.tmp", "kept");
        root.WriteFile(Path.Combine("sub", "move.txt"));

        _service.Execute(new CollectRequest(root.Path, "*.tmp"), null, TestContext.Current.CancellationToken);

        Assert.Equal(["keep.tmp", "move.txt"], root.FileNamesInRoot());
        Assert.Equal("kept", File.ReadAllText(root.Combine("keep.tmp")));
    }

    /// <summary>
    /// The legacy default ignore value was "." - regression guard so it never again
    /// behaves as a catch-all that stops the collect run from doing anything.
    /// </summary>
    [Theory]
    [InlineData(".")]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("*")]
    [InlineData("*.*")]
    public void DegenerateIgnorePatternsCollectEverything(string ignore)
    {
        using var root = new TempDirectory();
        root.WriteFile(Path.Combine("sub", "a.txt"));
        root.WriteFile(Path.Combine("sub", "b.jpg"));

        _service.Execute(new CollectRequest(root.Path, ignore), null, TestContext.Current.CancellationToken);

        Assert.Equal(["a.txt", "b.jpg"], root.FileNamesInRoot());
    }

    [Fact]
    public void ParsePatternsDropsDegenerateEntries() =>
        Assert.Equal(["*.tmp"], CollectService.ParsePatterns(" . , *.tmp , , * "));

    [Fact]
    public void CancellationLeavesDirectoryStructureIntact()
    {
        using var root = new TempDirectory();
        root.WriteFile(Path.Combine("sub", "a.txt"));
        using var cancelled = new CancellationTokenSource();
        cancelled.Cancel();

        var result = _service.Execute(new CollectRequest(root.Path, string.Empty), null, cancelled.Token);

        Assert.Equal(OperationOutcome.Cancelled, result.Outcome);
        Assert.True(Directory.Exists(root.Combine("sub")));
        Assert.True(File.Exists(root.Combine(Path.Combine("sub", "a.txt"))));
    }

    [Fact]
    public void RejectsEmptyPath()
    {
        var result = _service.Execute(new CollectRequest(string.Empty, string.Empty), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Invalid, result.Outcome);
        Assert.Equal(ValidationError.MissingPath, result.Error);
    }

    [Fact]
    public void GetFilesToCollectMatchesWhatARunMoves()
    {
        using var root = new TempDirectory();
        root.WriteFile(Path.Combine("sub", "a.txt"));
        root.WriteFile(Path.Combine("sub", "b.tmp"));

        Assert.Single(CollectService.GetFilesToCollect(root.Path, "*.tmp"));
        Assert.Equal(2, CollectService.GetFilesToCollect(root.Path, string.Empty).Count);
        Assert.Empty(CollectService.GetFilesToCollect(root.Combine("missing"), string.Empty));
    }
}
