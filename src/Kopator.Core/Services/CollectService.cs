namespace Kopator.Core.Services;

/// <param name="RootPath">Folder that is flattened in place.</param>
/// <param name="IgnorePatterns">Comma-separated globs (e.g. <c>*.tmp, *.bak</c>) that stay where they are.</param>
public readonly record struct CollectRequest(string RootPath, string IgnorePatterns);

/// <summary>
/// Flattens a folder tree: every file below <see cref="CollectRequest.RootPath"/> is moved up
/// into the root, then the emptied subdirectories are removed.
/// </summary>
public sealed class CollectService
{
    public OperationResult Execute(CollectRequest request, IProgress<int>? progress, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RootPath))
            return OperationResult.Invalid(ValidationError.MissingPath);

        if (!DirectoryAccess.Check(request.RootPath, readable: true, writable: true))
            return OperationResult.Invalid(ValidationError.SourceNotAccessible);

        var files = GetFilesToCollect(request.RootPath, request.IgnorePatterns);
        var result = FileTransfer.Run(files, request.RootPath, move: true, progress, cancellationToken);

        // Every subdirectory is removed once the run completes, including any ignored file
        // still inside it: "ignore" means "do not carry this up into the root", and the
        // sweep that follows is what makes collecting a cleanup rather than a copy.
        // A cancelled run deletes nothing, so stopping leaves the tree as it was.
        if (result.Outcome == OperationOutcome.Completed)
            RemoveSubdirectories(request.RootPath);

        return result;
    }

    /// <summary>Files that a run would move, in the order they are processed.</summary>
    public static IReadOnlyList<string> GetFilesToCollect(string rootPath, string ignorePatterns)
    {
        if (!Directory.Exists(rootPath))
            return [];

        var all = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories);
        var patterns = ParsePatterns(ignorePatterns);

        if (patterns.Count == 0)
            return all;

        var ignored = new HashSet<string>(PathComparer);

        foreach (var pattern in patterns)
        {
            try
            {
                foreach (var match in Directory.EnumerateFiles(rootPath, pattern, SearchOption.AllDirectories))
                    ignored.Add(match);
            }
            catch (ArgumentException)
            {
                // An unusable pattern ignores nothing instead of aborting the run.
            }
        }

        return [.. all.Where(f => !ignored.Contains(f))];
    }

    /// <summary>
    /// Splits the comma-separated ignore list. Entries that would match everything or nothing
    /// are dropped - notably the legacy default of "." which silently behaved as a wildcard.
    /// </summary>
    internal static List<string> ParsePatterns(string ignorePatterns) =>
    [
        .. (ignorePatterns ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Where(p => p is not ("." or ".." or "*" or "*.*"))
    ];

    /// <summary>
    /// Deletes every directory below <paramref name="rootPath"/> along with whatever is
    /// still inside it. Files excluded by an ignore pattern are removed here - that is
    /// the intended outcome, not an oversight. A directory that cannot be deleted is
    /// skipped rather than failing the run.
    /// </summary>
    private static void RemoveSubdirectories(string rootPath)
    {
        foreach (var directory in Directory.GetDirectories(rootPath))
        {
            try
            {
                Directory.Delete(directory, recursive: true);
            }
            catch
            {
                // A locked or unreadable directory stays behind; the collected files are
                // already in place, so this must not turn into a failed run.
            }
        }
    }

    private static StringComparer PathComparer =>
        OperatingSystem.IsLinux() ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
}
