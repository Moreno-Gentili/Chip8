using Chip8.Model.IO;
using System.IO;

namespace Chip8.Wpf.IO;

public class Cassette : ICassette
{
    private string romPath = string.Empty;
    public Cassette(string romPath)
    {
        Change(romPath);
    }

    public void Change(string romPath)
    {
        this.romPath = romPath;
    }

    public byte[] Load()
    {
        return File.ReadAllBytes(romPath);
    }
}
