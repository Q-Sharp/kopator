namespace Kopator.Core.Services;

/// <summary>The copy/move loop shared by <see cref="CopyService"/> and <see cref="CollectService"/>.</summary>
internal static class FileTransfer
{
    internal static OperationResult Run(
        IReadOnlyList<string> files,
        string destinationPath,
        bool move,
        IProgress<int>? progress,
        CancellationToken cancellationToken)
    {
        var processed = 0;
        var failed = 0;

        foreach (var file in files)
        {
            if (cancellationToken.IsCancellationRequested)
                return OperationResult.Cancelled(processed, failed);

            try
            {
                var target = Path.Combine(destinationPath, Path.GetFileName(file));

                // A file already sitting in the destination is left alone rather than
                // copied onto itself, which would truncate it.
                if (PathsEqual(file, target))
                    continue;

                if (move)
                    File.Move(file, target, overwrite: true);
                else
                    File.Copy(file, target, overwrite: true);

                processed++;
                progress?.Report(processed);
            }
            catch
            {
                failed++;
            }
        }

        return OperationResult.Completed(processed, failed);
    }

    /// <summary>
    /// Compares two paths the way the running file system does: case-sensitively on Linux,
    /// case-insensitively on Windows and macOS.
    /// </summary>
    private static bool PathsEqual(string left, string right)
    {
        var comparison = OperatingSystem.IsLinux()
            ? StringComparison.Ordinal
            : StringComparison.OrdinalIgnoreCase;

        return string.Equals(Path.GetFullPath(left), Path.GetFullPath(right), comparison);
    }
}
