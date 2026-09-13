using SkiaSharp;

namespace Kopator.Core.Services;

/// <summary>
/// Decodes thumbnails with SkiaSharp. Skia works without a windowing system, which keeps
/// catalog export usable headlessly and testable - unlike Avalonia's own Bitmap, which
/// needs an initialized render platform.
/// </summary>
public sealed class SkiaThumbnailProvider : IThumbnailProvider
{
    public string? TryCreateBase64Png(string filePath, int maxEdge)
    {
        if (maxEdge <= 0)
            return null;

        try
        {
            using var source = SKBitmap.Decode(filePath);

            if (source is null || source.Width <= 0 || source.Height <= 0)
                return null;

            // Only ever scale down; a small image is embedded at its original size.
            var scale = Math.Min(1.0, Math.Min((double)maxEdge / source.Width, (double)maxEdge / source.Height));
            var width = Math.Max(1, (int)Math.Round(source.Width * scale));
            var height = Math.Max(1, (int)Math.Round(source.Height * scale));

            using var resized = source.Resize(
                new SKImageInfo(width, height),
                new SKSamplingOptions(SKFilterMode.Linear, SKMipmapMode.Linear));

            if (resized is null)
                return null;

            using var image = SKImage.FromBitmap(resized);
            using var data = image.Encode(SKEncodedImageFormat.Png, 90);

            return data is null ? null : Convert.ToBase64String(data.ToArray());
        }
        catch
        {
            // Anything undecodable is simply not an image as far as the catalog is concerned.
            return null;
        }
    }
}
