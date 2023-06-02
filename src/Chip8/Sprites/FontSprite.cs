namespace Chip8.Sprites;

public class FontSprite : Sprite
{
    private const int fontSpriteWidth = 4;
    private const int fontSpriteHeight = 5;

    public FontSprite(Memory<byte> memory)
        : base(memory, fontSpriteWidth, fontSpriteHeight)
    {
        if (memory.Length != GetMemorySizeFromHeight(fontSpriteHeight))
        {
            throw new InvalidOperationException($"Font sprite should be {fontSpriteHeight} pixels high");
        }
    }

    public FontSprite(params byte[] rows)
        : base(PackBytes(rows), fontSpriteWidth, fontSpriteHeight)
    {
    }

    public static byte GetMemorySizeFromHeight(int height)
    {
        decimal factor = 8 / (decimal)fontSpriteWidth;
        return Convert.ToByte(Math.Ceiling(height / factor));
    }

    private static Memory<byte> PackBytes(byte[] rows)
    {
        byte[] packed = new byte[GetMemorySizeFromHeight(rows.Length)];
        for (int i = 0; i < rows.Length; i++)
        {
            int byteIndex = i / 2;
            byte value = rows[i];
            if (i % 2 == 0)
            {
                value &= 0XF0;
            }
            else
            {
                value >>= 4;
            }

            packed[byteIndex] |= value;
        }

        return packed;
    }
}