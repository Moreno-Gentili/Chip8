namespace Chip8.Model.IO;

public interface ICassette
{
    Memory<byte> Load();
}