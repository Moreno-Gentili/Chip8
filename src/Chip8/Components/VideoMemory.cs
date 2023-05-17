namespace Chip8.Hardware;

public class VideoMemory
{
    private readonly byte[,] buffer;

    public VideoMemory()
    {
        buffer = new byte[Columns, Rows];
    }
    
    public int Rows { get; } = 32;
    public int Columns { get; } = 64;
}