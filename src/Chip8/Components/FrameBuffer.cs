using Chip8.Model.Components;
using Chip8.Model.Sprites;

namespace Chip8.Components;

public class FrameBuffer : IFrameBuffer
{
    private const byte rows = 32;
    private const byte columns = 64;
    private bool[,] buffer;

    public FrameBuffer()
    {
        buffer = CreateBuffer();
    }

    public void Draw(ISprite sprite, int x, int y)
    {
        if (x < 0 || x >= columns)
        {
            throw new ArgumentException($"Must be between 0 and {columns - 1}", nameof(x));
        }

        if (y < 0 || y >= rows)
        {
            throw new ArgumentException($"Must be between 0 and {rows - 1}", nameof(y));
        }

        for (int cX = x; cX < x + sprite.Width; cX++)
        {
            for (int cY = y; cY < y + sprite.Height; cY++)
            {
                bool value = sprite[cX, cY];
                buffer[cX, cY] = buffer[cX, cY] ^ value;
            }
        }

    }

    public void Clear()
    {
        buffer = CreateBuffer();
    }

    private static bool[,] CreateBuffer()
    {
        return new bool[columns, rows];
    }
}