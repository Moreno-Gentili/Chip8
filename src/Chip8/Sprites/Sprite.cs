using Chip8.Model.Sprites;
namespace Chip8.Sprites;

public class Sprite : ISprite
{
    private const int maxHeight = 15;
    private Memory<byte> rows;

    public Sprite(Memory<byte> rows)
    {
        if (rows.Length is 0 or > maxHeight)
        {
            throw new ArgumentException($"Sprites must have an height between 1 and {maxHeight}", nameof(rows));
        }

        this.rows = rows;
    }
    
    public Sprite(params byte[] rows)
        : this((Memory<byte>)rows)
    {        
    }

    public byte Width => 8;
    public byte Height => Convert.ToByte(rows.Length);

    public Memory<byte> Memory => rows;

    public bool this[byte x, byte y]
    {
        get
        {
            EnsureCoordinatesAreWithinBounds(x, y);
            return (rows.Span[y] & (1 << (7 - x))) != 0;
        }
    }

    private void EnsureCoordinatesAreWithinBounds(byte x, byte y)
    {
        if (x < 0 || x >= Width)
        {
            throw new ArgumentException($"Horizontal offset must be between 0 and {Width - 1}", nameof(x));
        }

        if (y < 0 || y >= Height)
        {
            throw new ArgumentException($"Vertical offset must be between 0 and {Height - 1}", nameof(y));
        }
    }
}