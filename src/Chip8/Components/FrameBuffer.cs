using Chip8.Model.Components;
using Chip8.Model.Sprites;

namespace Chip8.Components;

public class FrameBuffer : IFrameBuffer
{
    private readonly Memory<byte> memory;

    public FrameBuffer(Memory<byte> memory)
    {
        if (memory.Length != MemorySize)
        {
            throw new ArgumentException($"Memory slice must be exatcly of size {MemorySize} bytes");
        }

        this.memory = memory;
        Clear();
    }

    public byte Width => 64;
    public byte Height => 32;
    private int MemoryWidth => Width / 8;
    private int MemorySize => MemoryWidth * Height;

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
        return (y * MemoryWidth) + (x / 8);
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