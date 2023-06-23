using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Web.IO;

public class Cassette : ICassette
{
    private HttpClient client;
    private Memory<byte>? memory;

    public Cassette()
    {
        client = new HttpClient();
    }

    public async Task Change(string romUrl)
    {
        memory = await client.GetByteArrayAsync(romUrl);
    }

    public async Task Change(Stream stream)
    {
        byte[] buffer = new byte[stream.Length];
        await stream.ReadExactlyAsync(buffer, 0, buffer.Length);
        memory = buffer;
    }

    public Memory<byte> Load()
    {
        return memory ?? throw new InvalidOperationException("Cassette not loaded");
    }
}
