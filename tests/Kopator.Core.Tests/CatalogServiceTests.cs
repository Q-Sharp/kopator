using Kopator.Core.Services;

namespace Kopator.Core.Tests;

public class CatalogServiceTests
{
    private const string FakeThumbnail = "QUJD";

    private readonly CatalogService _service = new(new FakeThumbnailProvider(FakeThumbnail));

    /// <summary>
    /// The stored setting is lower case ("csv") while the enum member is not. The legacy
    /// case-sensitive parse failed and silently produced HTML for a CSV request.
    /// </summary>
    [Theory]
    [InlineData("csv", CatalogExportType.Csv)]
    [InlineData("CSV", CatalogExportType.Csv)]
    [InlineData("html", CatalogExportType.Html)]
    [InlineData("HTML", CatalogExportType.Html)]
    [InlineData("nonsense", CatalogExportType.Csv)]
    [InlineData(null, CatalogExportType.Csv)]
    public void ParseExportTypeIgnoresCaseAndFallsBackToCsv(string? value, CatalogExportType expected) =>
        Assert.Equal(expected, CatalogService.ParseExportType(value));

    [Fact]
    public void CsvHasHeaderAndOneRowPerFile()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("a.txt");
        source.WriteFile("b.txt");
        var target = output.Combine("catalog.csv");

        var result = _service.Execute(
            new CatalogRequest(source.Path, target, "txt", CatalogExportType.Csv), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Completed, result.Outcome);
        Assert.Equal(2, result.ProcessedCount);

        var lines = File.ReadAllLines(target);
        Assert.Equal("Datei,Datum", lines[0]);
        Assert.Equal(3, lines.Length);
    }

    [Fact]
    public void CsvIsOrderedByLastWriteTime()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        var older = source.WriteFile("older.txt");
        var newer = source.WriteFile("newer.txt");
        File.SetLastWriteTime(older, new DateTime(2020, 1, 1, 12, 0, 0, DateTimeKind.Local));
        File.SetLastWriteTime(newer, new DateTime(2024, 6, 1, 12, 0, 0, DateTimeKind.Local));
        var target = output.Combine("catalog.csv");

        _service.Execute(new CatalogRequest(source.Path, target, "txt", CatalogExportType.Csv), null, TestContext.Current.CancellationToken);

        var lines = File.ReadAllLines(target);
        Assert.StartsWith("older.txt,2020-01-01 12:00:00", lines[1], StringComparison.Ordinal);
        Assert.StartsWith("newer.txt,2024-06-01 12:00:00", lines[2], StringComparison.Ordinal);
    }

    [Fact]
    public void CsvQuotesNamesContainingTheSeparator()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("a,b.txt");
        var target = output.Combine("catalog.csv");

        _service.Execute(new CatalogRequest(source.Path, target, "txt", CatalogExportType.Csv), null, TestContext.Current.CancellationToken);

        var line = File.ReadAllLines(target)[1];
        Assert.StartsWith("\"a,b.txt\",", line, StringComparison.Ordinal);
    }

    [Fact]
    public void HtmlEmbedsThumbnailForImagesOnly()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("picture.png");
        var target = output.Combine("catalog.html");

        _service.Execute(new CatalogRequest(source.Path, target, string.Empty, CatalogExportType.Html), null, TestContext.Current.CancellationToken);

        var html = File.ReadAllText(target);
        Assert.Contains($"<img src=\"data:image/png;base64,{FakeThumbnail}\"", html, StringComparison.Ordinal);
    }

    [Fact]
    public void HtmlLeavesPreviewCellEmptyForNonImages()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("notes.txt");
        var target = output.Combine("catalog.html");

        var service = new CatalogService(new FakeThumbnailProvider(null));
        service.Execute(new CatalogRequest(source.Path, target, string.Empty, CatalogExportType.Html), null, TestContext.Current.CancellationToken);

        var html = File.ReadAllText(target);
        Assert.DoesNotContain("<img", html, StringComparison.Ordinal);
        Assert.Contains("<td>notes.txt</td>", html, StringComparison.Ordinal);
        Assert.Contains("<td></td>", html, StringComparison.Ordinal);
    }

    [Fact]
    public void HtmlEscapesFileNames()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("a&b.txt");
        var target = output.Combine("catalog.html");

        var service = new CatalogService(new FakeThumbnailProvider(null));
        service.Execute(new CatalogRequest(source.Path, target, string.Empty, CatalogExportType.Html), null, TestContext.Current.CancellationToken);

        var html = File.ReadAllText(target);
        Assert.Contains("<td>a&amp;b.txt</td>", html, StringComparison.Ordinal);
    }

    [Fact]
    public void FileTypeFilterRestrictsTheListing()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("a.jpg");
        source.WriteFile("b.txt");
        var target = output.Combine("catalog.csv");

        var result = _service.Execute(
            new CatalogRequest(source.Path, target, "jpg", CatalogExportType.Csv), null, TestContext.Current.CancellationToken);

        Assert.Equal(1, result.ProcessedCount);
        Assert.Contains("a.jpg", File.ReadAllText(target), StringComparison.Ordinal);
    }

    [Fact]
    public void EmptyFileTypeListsEverythingRecursively()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("a.jpg");
        source.WriteFile(Path.Combine("sub", "b.txt"));
        var target = output.Combine("catalog.csv");

        var result = _service.Execute(
            new CatalogRequest(source.Path, target, string.Empty, CatalogExportType.Csv), null, TestContext.Current.CancellationToken);

        Assert.Equal(2, result.ProcessedCount);
    }

    /// <summary>Progress must advance for HTML too - the legacy code only reported it for CSV.</summary>
    [Theory]
    [InlineData(CatalogExportType.Csv)]
    [InlineData(CatalogExportType.Html)]
    public void ReportsProgressForBothExportTypes(CatalogExportType exportType)
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("a.txt");
        source.WriteFile("b.txt");
        var reported = new List<int>();

        _service.Execute(
            new CatalogRequest(source.Path, output.Combine("catalog.out"), string.Empty, exportType),
            new SynchronousProgress<int>(reported.Add),
            TestContext.Current.CancellationToken);

        Assert.Equal([1, 2], reported);
    }

    [Fact]
    public void CancellationWritesNoFile()
    {
        using var source = new TempDirectory();
        using var output = new TempDirectory();
        source.WriteFile("a.txt");
        var target = output.Combine("catalog.csv");
        using var cancelled = new CancellationTokenSource();
        cancelled.Cancel();

        var result = _service.Execute(
            new CatalogRequest(source.Path, target, string.Empty, CatalogExportType.Csv), null, cancelled.Token);

        Assert.Equal(OperationOutcome.Cancelled, result.Outcome);
        Assert.False(File.Exists(target));
    }

    [Theory]
    [InlineData("", "target.csv")]
    [InlineData("source", "")]
    public void RejectsEmptyPaths(string source, string target)
    {
        var result = _service.Execute(
            new CatalogRequest(source, target, string.Empty, CatalogExportType.Csv), null, TestContext.Current.CancellationToken);

        Assert.Equal(OperationOutcome.Invalid, result.Outcome);
        Assert.Equal(ValidationError.MissingPath, result.Error);
    }

    [Fact]
    public void CountFilesMatchesWhatARunLists()
    {
        using var source = new TempDirectory();
        source.WriteFile("a.jpg");
        source.WriteFile(Path.Combine("sub", "b.jpg"));
        source.WriteFile("c.txt");

        Assert.Equal(2, CatalogService.CountFiles(source.Path, "jpg"));
        Assert.Equal(3, CatalogService.CountFiles(source.Path, string.Empty));
        Assert.Equal(0, CatalogService.CountFiles(source.Combine("missing"), string.Empty));
    }

    private sealed class FakeThumbnailProvider(string? result) : IThumbnailProvider
    {
        public string? TryCreateBase64Png(string filePath, int maxEdge) => result;
    }
}
