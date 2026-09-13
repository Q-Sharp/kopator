namespace Kopator.Core.Services;

/// <summary>Probes what a directory actually allows, by trying rather than by inspecting ACLs.</summary>
public static class DirectoryAccess
{
    /// <summary>
    /// Returns <c>true</c> when <paramref name="path"/> supports the requested access.
    /// A write probe creates and immediately removes a temporary file, which is the only
    /// portable way to learn whether a write would succeed.
    /// </summary>
    public static bool Check(string path, bool readable, bool writable)
    {
        try
        {
            if (writable)
            {
                var probe = Path.Combine(path, Path.GetRandomFileName());
                using (File.Create(probe, 1, FileOptions.DeleteOnClose))
                {
                }
            }

            if (readable)
                Directory.EnumerateFileSystemEntries(path).FirstOrDefault();

            return true;
        }
        catch
        {
            return false;
        }
    }
}
