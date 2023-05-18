using Chip8.Model.Sprites;
namespace Chip8.Sprites;

public class Sprite : ISprite
{
    private const int maxHeight = 15;
    private byte[] lines;

    public Sprite(params byte[] lines)
    {
        if (lines.Length > maxHeight)
        {
            throw new ArgumentException($"Sprites with an height greater than {maxHeight} are not supported", nameof(lines));
        }

        this.lines = lines;
    }

    public int Width => 8;
    public int Height => lines.Length;

    public bool this[int x, int y]
    {
        get
        {
            if (x < 0 || x > Width)
            {
                throw new ArgumentException($"Must be between 0 and {Width}", nameof(x));
            }

            if (y < 0 || y > Height)
            {
                throw new ArgumentException($"Must be between 0 and {Height}", nameof(y));
            }

            return (lines[y] & (1 << x)) != 0;
        }
    }
}