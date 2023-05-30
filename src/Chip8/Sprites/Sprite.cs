using Chip8.Model.Sprites;
namespace Chip8.Sprites;

public class Sprite : ISprite
{
    private const int maxHeight = 15;
    private byte[] lines;

    public Sprite(params byte[] lines)
    {
        if (lines.Length is 0 or > maxHeight)
        {
            throw new ArgumentException($"Sprites must have an height between 1 and {maxHeight}", nameof(lines));
        }

        this.lines = lines;
    }

    public byte Width => 8;
    public byte Height => Convert.ToByte(lines.Length);

    public bool this[byte x, byte y]
    {
        get
        {
            EnsureCoordinatesAreWithinBounds(x, y);
            return (lines[y] & (1 << (7 - x))) != 0;
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