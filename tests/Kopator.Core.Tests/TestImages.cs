using System.Buffers.Binary;
using System.IO.Compression;

namespace Kopator.Core.Tests;

/// <summary>
/// Builds PNG files byte by byte, so image tests do not depend on a checked-in binary
/// or on the decoder under test.
/// </summary>
public static class TestImages
{
    public static byte[] Png(int width, int height)
    {
        var raw = new byte[height * (1 + (width * 3))];
        var offset = 0;

        for (var y = 0; y < height; y++)
        {
            raw[offset++] = 0; // filter type: none
            for (var x = 0; x < width; x++)
            {
                raw[offset++] = (byte)(x * 4);
                raw[offset++] = (byte)(y * 4);
                raw[offset++] = 0x80;
            }
        }

        var header = new byte[13];
        BinaryPrimitives.WriteInt32BigEndian(header.AsSpan(0, 4), width);
        BinaryPrimitives.WriteInt32BigEndian(header.AsSpan(4, 4), height);
        header[8] = 8; // bit depth
        header[9] = 2; // colour type: truecolour

        using var png = new MemoryStream();
        png.Write([0x89, (byte)'P', (byte)'N', (byte)'G', 0x0D, 0x0A, 0x1A, 0x0A]);
        WriteChunk(png, "IHDR", header);
        WriteChunk(png, "IDAT", Deflate(raw));
        WriteChunk(png, "IEND", []);

        return png.ToArray();
    }

    private static byte[] Deflate(byte[] data)
    {
        using var compressed = new MemoryStream();
        using (var deflate = new ZLibStream(compressed, CompressionLevel.Optimal, leaveOpen: true))
            deflate.Write(data);

        return compressed.ToArray();
    }

    private static void WriteChunk(Stream target, string type, byte[] data)
    {
        var typeBytes = System.Text.Encoding.ASCII.GetBytes(type);
        var length = new byte[4];
        BinaryPrimitives.WriteInt32BigEndian(length, data.Length);

        target.Write(length);
        target.Write(typeBytes);
        target.Write(data);

        var crc = new byte[4];
        BinaryPrimitives.WriteUInt32BigEndian(crc, Crc32([.. typeBytes, .. data]));
        target.Write(crc);
    }

    private static uint Crc32(byte[] data)
    {
        var crc = 0xFFFFFFFFu;

        foreach (var b in data)
        {
            crc ^= b;
            for (var i = 0; i < 8; i++)
                crc = (crc >> 1) ^ (0xEDB88320u & (uint)-(crc & 1));
        }

        return crc ^ 0xFFFFFFFFu;
    }
}
