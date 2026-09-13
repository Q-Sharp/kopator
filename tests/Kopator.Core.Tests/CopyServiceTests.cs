using Kopator.Core.Services;

namespace Kopator.Core.Tests;

public class CopyServiceTests
{
    private readonly CopyService _service = new();

    [Fact]
    public void CopiesFilesAndLeavesSourceIntact()
    {
        using var source = new TempDirectory();
        using var destination = new TempDirectory();
        source.WriteFile("a.txt", "content-a");
        source.WriteFile("b.txt", "content-b");

        var result = _service.Execute(new CopyRequest(source.Path, destination.Path, Move: false), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Completed, result.Outcome);
        Assert.Equal(2, result.ProcessedCount);
        Assert.Equal(["a.txt", "b.txt"], destination.FileNamesInRoot());
        Assert.Equal(["a.txt", "b.txt"], source.FileNamesInRoot());
        Assert.Equal("content-a", File.ReadAllText(destination.Combine("a.txt")));
    }

    [Fact]
    public void MoveRemovesFilesFromSource()
    {
        using var source = new TempDirectory();
        using var destination = new TempDirectory();
        source.WriteFile("a.txt");

        var result = _service.Execute(new CopyRequest(source.Path, destination.Path, Move: true), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Completed, result.Outcome);
        Assert.Equal(["a.txt"], destination.FileNamesInRoot());
        Assert.Empty(source.FileNamesInRoot());
    }

    [Fact]
    public void DoesNotDescendIntoSubdirectories()
    {
        using var source = new TempDirectory();
        using var destination = new TempDirectory();
        source.WriteFile("top.txt");
        source.WriteFile(Path.Combine("sub", "nested.txt"));

        _service.Execute(new CopyRequest(source.Path, destination.Path, Move: false), null, TestContext.Current.CancellationToken);

        Assert.Equal(["top.txt"], destination.FileNamesInRoot());
    }

    [Fact]
    public void SameSourceAndDestinationLeavesFilesUntouched()
    {
        using var directory = new TempDirectory();
        directory.WriteFile("a.txt", "original");

        var result = _service.Execute(new CopyRequest(directory.Path, directory.Path, Move: false), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Completed, result.Outcome);
        Assert.Equal(0, result.ProcessedCount);
        Assert.Equal("original", File.ReadAllText(directory.Combine("a.txt")));
    }

    [Fact]
    public void OverwritesExistingFileInDestination()
    {
        using var source = new TempDirectory();
        using var destination = new TempDirectory();
        source.WriteFile("a.txt", "new");
        destination.WriteFile("a.txt", "old");

        _service.Execute(new CopyRequest(source.Path, destination.Path, Move: false), null, TestContext.Current.CancellationToken);

        Assert.Equal("new", File.ReadAllText(destination.Combine("a.txt")));
    }

    [Fact]
    public void CreatesMissingDestinationDirectory()
    {
        using var source = new TempDirectory();
        using var parent = new TempDirectory();
        source.WriteFile("a.txt");
        var destination = parent.Combine("created-on-demand");

        var result = _service.Execute(new CopyRequest(source.Path, destination, Move: false), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Completed, result.Outcome);
        Assert.True(File.Exists(Path.Combine(destination, "a.txt")));
    }

    [Theory]
    [InlineData("", "somewhere")]
    [InlineData("somewhere", "")]
    [InlineData("   ", "   ")]
    public void RejectsEmptyPaths(string source, string destination)
    {
        var result = _service.Execute(new CopyRequest(source, destination, Move: false), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Invalid, result.Outcome);
        Assert.Equal(ValidationError.MissingPath, result.Error);
    }

    [Fact]
    public void RejectsUnreadableSource()
    {
        using var destination = new TempDirectory();

        var result = _service.Execute(
            new CopyRequest(Path.Combine(Path.GetTempPath(), "kopator-does-not-exist"), destination.Path, Move: false),
            null,
            TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Invalid, result.Outcome);
        Assert.Equal(ValidationError.SourceNotAccessible, result.Error);
    }

    [Fact]
    public void CancellationStopsBeforeCopying()
    {
        using var source = new TempDirectory();
        using var destination = new TempDirectory();
        source.WriteFile("a.txt");
        using var cancelled = new CancellationTokenSource();
        cancelled.Cancel();

        var result = _service.Execute(new CopyRequest(source.Path, destination.Path, Move: false), null, cancelled.Token);

        Assert.Equal(OperationOutcome.Cancelled, result.Outcome);
        Assert.Empty(destination.FileNamesInRoot());
    }

    [Fact]
    public void ReportsProgressPerFile()
    {
        using var source = new TempDirectory();
        using var destination = new TempDirectory();
        source.WriteFile("a.txt");
        source.WriteFile("b.txt");
        var reported = new List<int>();

        _service.Execute(
            new CopyRequest(source.Path, destination.Path, Move: false),
            new SynchronousProgress<int>(reported.Add),
            TestContext.Current.CancellationToken);

        Assert.Equal([1, 2], reported);
    }

    [Fact]
    public void CountFilesMatchesWhatARunProcesses()
    {
        using var source = new TempDirectory();
        source.WriteFile("a.txt");
        source.WriteFile("b.txt");
        source.WriteFile(Path.Combine("sub", "c.txt"));

        Assert.Equal(2, CopyService.CountFiles(source.Path));
        Assert.Equal(0, CopyService.CountFiles(source.Combine("missing")));
    }
}
