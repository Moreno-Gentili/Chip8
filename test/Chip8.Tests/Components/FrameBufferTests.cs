using Chip8.Components;
using Chip8.Sprites;

namespace Chip8.Tests;

public class FrameBufferTests
{
    private readonly Sprite sprite = new Sprite(0xFF, 0xFF, 0xFF, 0xFF, 0xFF);
    private readonly Memory<byte> memory = new byte[64 * 32 / 8];

    [TestCase(64, 0)]
    [TestCase(65, 0)]
    [TestCase(0, 32)]
    [TestCase(0, 33)]
    [TestCase(64, 32)]
    [TestCase(byte.MaxValue, byte.MaxValue)]
    public void Draw_ShouldThrow_WhenSpriteIsDrawnOutOfBounds(byte x, byte y)
    {
        FrameBuffer frameBuffer = new(memory);
        Assert.Throws<ArgumentException>(() => frameBuffer.Draw(sprite, x, y));
    }

    [TestCase(0, 0)]
    [TestCase(60, 28)]
    [TestCase(63, 31)]
    public void Draw_ShouldSucceed_WhenSpriteIsCompletelyOrPartiallyInsideTheFrameBuffer(byte x, byte y)
    {
        FrameBuffer frameBuffer = new(memory);
        frameBuffer.Draw(sprite, x, y);

        byte upperX = Convert.ToByte(Math.Min(x + sprite.Width, frameBuffer.Width));
        byte upperY = Convert.ToByte(Math.Min(y + sprite.Height, frameBuffer.Height));

        for (; x < upperX; x++)
        {
            for (; y < upperY; y++)
            {
                Assert.IsTrue(frameBuffer[x, y]);
            }
        }
    }
}