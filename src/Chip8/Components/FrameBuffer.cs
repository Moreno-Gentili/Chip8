using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.Sprites;

namespace Chip8.Components;

public class FrameBuffer : MemoryComponent, IFrameBuffer
{
    public FrameBuffer(Memory<byte> memory)
        : base(memory, MemorySize)
    {
        Clear();
    }

    public static int MemorySize => WidthInBytes * Height;
    private static byte Width => 64;
    private static byte Height => 32;
    private static int WidthInBytes => Width / 8;
    int IFrameBuffer.Width => Width;
    int IFrameBuffer.Height => Height;

    internal static FrameBuffer From(Memory<byte> memory)
    {
        return new FrameBuffer(memory.Chunk(MemorySize));
    }

    public void Draw(ISprite sprite, byte x, byte y)
    {
        EnsureCoordinatesAreWithinBounds(x, y);

        byte offsetX = x;
        byte offsetY = y;
        byte upperX = Convert.ToByte(Math.Min(x + sprite.Width, Width));
        byte upperY = Convert.ToByte(Math.Min(y + sprite.Height, Height));

        for (x = offsetX; x < upperX; x++)
        {
            for (y = offsetY; y < upperY; y++)
            {
                bool value = sprite[Convert.ToByte(x - offsetX), Convert.ToByte(y - offsetY)];
                XorPixel(x, y, value);
            }
        }
    }

    public bool this[byte x, byte y]
    {
        get
        {
            EnsureCoordinatesAreWithinBounds(x, y);
            return GetPixel(x, y);
        }
    }
    
    public void Clear()
    {
        for (int i = 0; i < MemorySize; i++)
        {
            memory.Span[i] = 0;
        }
    }

    private bool GetPixel(byte x, byte y)
    {
        int pixelPosition = GetPixelPosition(x, y);
        byte pixelByte = memory.Span[pixelPosition];
        byte mask = GetPixelMask(x, true);
        return (pixelByte & mask) != 0;
    }

    private void XorPixel(byte x, byte y, bool value)
    {
        int bytePosition = GetPixelPosition(x, y);
        byte mask = GetPixelMask(x, value);
        memory.Span[bytePosition] ^= mask;
    }

    private int GetPixelPosition(byte x, byte y)
    {
        return (y * WidthInBytes) + (x / 8);
    }

    private byte GetPixelMask(byte x, bool value)
    {
        if (!value)
        {
            return byte.MinValue;
        }

        return Convert.ToByte(1 << (7 - (x % 8)));
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