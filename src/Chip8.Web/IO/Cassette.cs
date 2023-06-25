using Chip8.Model.IO;
using Force.Crc32;

namespace Chip8.Web.IO;

public class Cassette : ICassette
{
    private HttpClient client;
    private Memory<byte>? memory;
    private string? hash;

    public Cassette()
    {
        client = new HttpClient();
    }

    public string? Crc32 => hash;

    public void Eject()
    {
        memory = null;
    }

    public async Task Change(string romUrl)
    {
        byte[] buffer = await client.GetByteArrayAsync(romUrl);
        UpdateMemory(buffer);
    }

    public async Task Change(Stream stream)
    {
        byte[] buffer = new byte[stream.Length];
        await stream.ReadExactlyAsync(buffer, 0, buffer.Length);
        UpdateMemory(buffer);
    }

    public Memory<byte> Load()
    {
        return memory ?? throw new InvalidOperationException("Cassette not loaded");
    }

    private void UpdateMemory(byte[] buffer)
    {
        memory = buffer;
        hash = CalculateHash(buffer);
    }

    private static string CalculateHash(byte[] buffer)
    {
        uint hash = Crc32Algorithm.Compute(buffer);
        buffer = new byte[]
        {
            Convert.ToByte(hash >> 24),
            Convert.ToByte((hash >> 16) & 0xFF),
            Convert.ToByte((hash >> 8) & 0xFF),
            Convert.ToByte(hash & 0xFF)

        };

        return Convert.ToHexString(buffer);
    }
}
