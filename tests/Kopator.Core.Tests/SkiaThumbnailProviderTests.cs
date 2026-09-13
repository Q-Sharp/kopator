using Kopator.Core.Services;

namespace Kopator.Core.Tests;

/// <summary>
/// Exercises the real decoder headlessly - this is the code path that used to be
/// System.Drawing and would throw <see cref="PlatformNotSupportedException"/> on Linux.
/// </summary>
public class SkiaThumbnailProviderTests
{
    private readonly SkiaThumbnailProvider _provider = new();

    [Fact]
    public void DecodesAPngIntoBase64()
    {
        using var directory = new TempDirectory();
        var file = directory.Combine("image.png");
        File.WriteAllBytes(file, TestImages.Png(64, 64));

        var thumbnail = _provider.TryCreateBase64Png(file, 32);

        Assert.NotNull(thumbnail);
        Assert.True(IsPng(Convert.FromBase64String(thumbnail)));
    }

    [Fact]
    public void ScalesDownToTheRequestedEdge()
    {
        using var directory = new TempDirectory();
        var file = directory.Combine("image.png");
        File.WriteAllBytes(file, TestImages.Png(200, 100));

        var thumbnail = _provider.TryCreateBase64Png(file, 50);

        Assert.NotNull(thumbnail);
        var (width, height) = PngSize(Convert.FromBase64String(thumbnail));
        Assert.Equal(50, width);
        Assert.Equal(25, height);
    }

    [Fact]
    public void DoesNotScaleUpSmallImages()
    {
        using var directory = new TempDirectory();
        var file = directory.Combine("image.png");
        File.WriteAllBytes(file, TestImages.Png(16, 16));

        var thumbnail = _provider.TryCreateBase64Png(file, 120);

        Assert.NotNull(thumbnail);
        var (width, height) = PngSize(Convert.FromBase64String(thumbnail));
        Assert.Equal(16, width);
        Assert.Equal(16, height);
    }

    [Fact]
    public void ReturnsNullForNonImages()
    {
        using var directory = new TempDirectory();
        var file = directory.WriteFile("notes.txt", "definitely not an image");

        Assert.Null(_provider.TryCreateBase64Png(file, 120));
    }

    [Fact]
    public void ReturnsNullForMissingFiles()
    {
        using var directory = new TempDirectory();

        Assert.Null(_provider.TryCreateBase64Png(directory.Combine("missing.png"), 120));
    }

    private static bool IsPng(byte[] bytes) =>
        bytes.Length > 8 && bytes[0] == 0x89 && bytes[1] == 'P' && bytes[2] == 'N' && bytes[3] == 'G';

    /// <summary>Reads width and height out of a PNG's IHDR chunk.</summary>
    private static (int Width, int Height) PngSize(byte[] bytes) =>
        (System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(16, 4)),
         System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(20, 4)));
}
