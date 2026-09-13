namespace Kopator.Core.Services;

/// <summary>Produces preview images for the HTML catalog.</summary>
public interface IThumbnailProvider
{
    /// <summary>
    /// Returns a base64-encoded PNG thumbnail of <paramref name="filePath"/>, or <c>null</c>
    /// when the file is not an image this provider can decode.
    /// </summary>
    string? TryCreateBase64Png(string filePath, int maxEdge);
}
