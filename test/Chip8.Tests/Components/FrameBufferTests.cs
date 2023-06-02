using Chip8.Components;
using Chip8.Model.Components;
using Chip8.Sprites;

namespace Chip8.Tests;

public class FrameBufferTests
{
    private readonly Sprite sprite = new Sprite(0xFF, 0xFF, 0xFF, 0xFF, 0xFF);
    private readonly Memory<byte> memory = new byte[64 * 32 / 8];

    [TestCase(0, 0)]
    [TestCase(60, 28)]
    [TestCase(63, 31)]
    public void Draw_ShouldSucceed_WrappingCoordinatesWhenOutOfBounds(byte x, byte y)
    {
        IFrameBuffer frameBuffer = new FrameBuffer(memory);
        frameBuffer.Draw(sprite, x, y);

        byte offsetX = Convert.ToByte(x % frameBuffer.Width);
        byte offsetY = Convert.ToByte(y % frameBuffer.Height);
        byte upperX = Convert.ToByte(Math.Min(x + sprite.Width, frameBuffer.Width));
        byte upperY = Convert.ToByte(Math.Min(y + sprite.Height, frameBuffer.Height));

        for (x = offsetX; x < upperX; x++)
        {
            for (y = offsetY; y < upperY; y++)
            {
                Assert.IsTrue(frameBuffer[x, y]);
            }
        }
    }
}