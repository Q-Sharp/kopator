namespace Kopator.Core.Tests;

/// <summary>A throwaway directory tree that removes itself at the end of a test.</summary>
public sealed class TempDirectory : IDisposable
{
    public TempDirectory()
    {
        Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "kopator-tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path);
    }

    public string Path { get; }

    /// <summary>Creates a file (and any missing parent directories) relative to the root.</summary>
    public string WriteFile(string relativePath, string content = "x")
    {
        var full = System.IO.Path.Combine(Path, relativePath);
        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(full)!);
        File.WriteAllText(full, content);
        return full;
    }

    public string Combine(string relativePath) => System.IO.Path.Combine(Path, relativePath);

    public string[] FileNamesInRoot() =>
        [.. Directory.GetFiles(Path).Select(System.IO.Path.GetFileName).OrderBy(n => n, StringComparer.Ordinal)!];

    public void Dispose()
    {
        try
        {
            Directory.Delete(Path, recursive: true);
        }
        catch
        {
            // A leftover temp directory must not fail a test run.
        }
    }
}
