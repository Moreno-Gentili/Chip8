using Chip8.Model.Components;
using Chip8.Model.Sprites;
using CommunityToolkit.HighPerformance;

namespace Chip8.Components;

public class FrameBuffer : IFrameBuffer
{
    private bool[,] buffer;

    public FrameBuffer()
    {
        buffer = CreateBuffer();
    }

    public byte Width => 64;
    public byte Height => 32;

    public void Draw(ISprite sprite, byte x, byte y)
    {
        EnsureCoordinatesAreWithinBounds(x, y);

        byte offsetX = x;
        byte offsetY = y;
        byte upperX = Convert.ToByte(Math.Min(x + sprite.Width, Width));
        byte upperY = Convert.ToByte(Math.Min(y + sprite.Height, Height));

        for (; x < upperX; x++)
        {
            for (; y < upperY; y++)
            {
                buffer[x, y] ^= sprite[Convert.ToByte(x - offsetX), Convert.ToByte(y - offsetY)];
            }
        }
    }

    public bool this[byte x, byte y]
    {
        get
        {
            EnsureCoordinatesAreWithinBounds(x, y);
            return buffer[x, y];
        }
    }
    
    public void Clear()
    {
        buffer = CreateBuffer();
    }

    private bool[,] CreateBuffer()
    {
        return new bool[Width, Height];
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