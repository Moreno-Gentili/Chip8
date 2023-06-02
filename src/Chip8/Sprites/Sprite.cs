using Chip8.Model.Sprites;
namespace Chip8.Sprites;

public class Sprite : ISprite
{
    private const int maxHeight = 15;
    private readonly Memory<byte> rows;
    private const int spriteWidth = 8;

    public Sprite(Memory<byte> memory)
        : this(memory, spriteWidth, Convert.ToByte(memory.Length))
    {
    }
    
    public Sprite(params byte[] rows)
        : this((Memory<byte>)rows, spriteWidth, Convert.ToByte(rows.Length))
    {
    }

    protected Sprite(Memory<byte> memory, byte width, byte height)
    {
        if (height is 0 or > maxHeight)
        {
            throw new ArgumentException($"Sprites must have an height between 1 and {maxHeight}", nameof(memory));
        }

        this.rows = memory;
        this.Width = width;
        this.Height = height;
    }

    public virtual byte Width { get; }
    public byte Height { get; }

    public virtual Memory<byte> Memory => rows;

    public bool this[byte x, byte y]
    {
        get
        {
            EnsureCoordinatesAreWithinBounds(x, y);

            int offset = (y * Width) + x;
            int byteIndex = offset / 8;
            int bitIndex = offset % 8;
            int mask = (1 << (7 - bitIndex));
            return (rows.Span[byteIndex] & mask) != 0;
        }
    }

    private void EnsureCoordinatesAreWithinBounds(byte x, byte y)
    {
        if (x >= Width)
        {
            throw new ArgumentException($"Horizontal offset must be between 0 and {Width - 1}", nameof(x));
        }

        if (y >= Height)
        {
            throw new ArgumentException($"Vertical offset must be between 0 and {Height - 1}", nameof(y));
        }
    }
}