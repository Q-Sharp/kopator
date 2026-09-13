using System.Globalization;
using System.Net;
using System.Text;

namespace Kopator.Core.Services;

/// <param name="SourcePath">Folder that is catalogued recursively.</param>
/// <param name="DestinationFile">File the catalog is written to.</param>
/// <param name="FileType">Extension to restrict the listing to, without a dot; empty lists everything.</param>
/// <param name="ExportType">Output format.</param>
public readonly record struct CatalogRequest(
    string SourcePath,
    string DestinationFile,
    string FileType,
    CatalogExportType ExportType);

/// <summary>Writes a listing of a folder tree as CSV, or as HTML with embedded thumbnails.</summary>
public sealed class CatalogService(IThumbnailProvider thumbnails)
{
    /// <summary>Edge length of the thumbnails embedded in an HTML catalog.</summary>
    public const int ThumbnailSize = 120;

    private const string TimestampFormat = "yyyy-MM-dd HH:mm:ss";

    private readonly IThumbnailProvider _thumbnails = thumbnails;

    public OperationResult Execute(CatalogRequest request, IProgress<int>? progress, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.SourcePath) || string.IsNullOrWhiteSpace(request.DestinationFile))
            return OperationResult.Invalid(ValidationError.MissingPath);

        if (!DirectoryAccess.Check(request.SourcePath, readable: true, writable: false))
            return OperationResult.Invalid(ValidationError.SourceNotAccessible);

        var entries = GetEntries(request.SourcePath, request.FileType);

        var output = new StringBuilder();
        var processed = 0;

        if (request.ExportType == CatalogExportType.Csv)
        {
            AppendCsvHeader(output);

            foreach (var entry in entries)
            {
                if (cancellationToken.IsCancellationRequested)
                    return OperationResult.Cancelled(processed);

                AppendCsvRow(output, entry);
                processed++;
                progress?.Report(processed);
            }
        }
        else
        {
            AppendHtmlHeader(output);

            foreach (var entry in entries)
            {
                if (cancellationToken.IsCancellationRequested)
                    return OperationResult.Cancelled(processed);

                AppendHtmlRow(output, entry);
                processed++;
                progress?.Report(processed);
            }

            AppendHtmlFooter(output);
        }

        try
        {
            var directory = Path.GetDirectoryName(Path.GetFullPath(request.DestinationFile));

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(request.DestinationFile, output.ToString(), Encoding.UTF8);
        }
        catch
        {
            return OperationResult.Invalid(ValidationError.DestinationNotAccessible);
        }

        return OperationResult.Completed(processed);
    }

    /// <summary>Number of files a run would list, so the caller can size a progress bar.</summary>
    public static int CountFiles(string sourcePath, string fileType) => GetEntries(sourcePath, fileType).Count;

    /// <summary>
    /// Parses the export type stored in the settings file. Matching ignores case, so a stored
    /// "csv" selects <see cref="CatalogExportType.Csv"/> instead of silently falling back to HTML.
    /// </summary>
    public static CatalogExportType ParseExportType(string? value) =>
        Enum.TryParse<CatalogExportType>(value, ignoreCase: true, out var parsed) ? parsed : CatalogExportType.Csv;

    private static List<CatalogEntry> GetEntries(string sourcePath, string fileType)
    {
        if (!Directory.Exists(sourcePath))
            return [];

        var pattern = string.IsNullOrWhiteSpace(fileType) ? "*" : $"*.{fileType.Trim().TrimStart('.')}";

        try
        {
            return [.. Directory
                .EnumerateFiles(sourcePath, pattern, SearchOption.AllDirectories)
                .Select(path => new CatalogEntry(path, File.GetLastWriteTime(path)))
                .OrderBy(entry => entry.LastWriteTime)];
        }
        catch (ArgumentException)
        {
            return [];
        }
    }

    private static void AppendCsvHeader(StringBuilder output) => output.AppendLine("Datei,Datum");

    private static void AppendCsvRow(StringBuilder output, CatalogEntry entry)
    {
        output.Append(Csv(Path.GetFileName(entry.Path)))
              .Append(',')
              .AppendLine(Csv(entry.LastWriteTime.ToString(TimestampFormat, CultureInfo.InvariantCulture)));
    }

    /// <summary>Quotes a CSV field so that commas and quotes in file names cannot shift columns.</summary>
    private static string Csv(string value) =>
        value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r')
            ? $"\"{value.Replace("\"", "\"\"")}\""
            : value;

    private static void AppendHtmlHeader(StringBuilder output)
    {
        output.AppendLine("<!DOCTYPE html>");
        output.AppendLine("<html lang=\"de\">");
        output.AppendLine("<head>");
        output.AppendLine("<meta charset=\"utf-8\" />");
        output.AppendLine("<title>Katalog</title>");
        output.AppendLine("<style>");
        output.AppendLine("body { font-family: sans-serif; }");
        output.AppendLine("table { width: 100%; border-collapse: collapse; }");
        output.AppendLine("th, td { border-bottom: 1px solid #ccc; padding: 4px; text-align: left; }");
        output.AppendLine("</style>");
        output.AppendLine("</head>");
        output.AppendLine("<body>");
        output.AppendLine("<table>");
        output.AppendLine("<tr><th>Datei</th><th>Datum</th><th>Vorschau</th></tr>");
    }

    private void AppendHtmlRow(StringBuilder output, CatalogEntry entry)
    {
        var name = WebUtility.HtmlEncode(Path.GetFileName(entry.Path));
        var timestamp = entry.LastWriteTime.ToString(TimestampFormat, CultureInfo.InvariantCulture);
        var thumbnail = _thumbnails.TryCreateBase64Png(entry.Path, ThumbnailSize);

        var preview = thumbnail is null
            ? string.Empty
            : $"<img src=\"data:image/png;base64,{thumbnail}\" alt=\"{name}\" />";

        output.AppendLine($"<tr><td>{name}</td><td>{timestamp}</td><td>{preview}</td></tr>");
    }

    private static void AppendHtmlFooter(StringBuilder output)
    {
        output.AppendLine("</table>");
        output.AppendLine("</body>");
        output.AppendLine("</html>");
    }

    private readonly record struct CatalogEntry(string Path, DateTime LastWriteTime);
}
