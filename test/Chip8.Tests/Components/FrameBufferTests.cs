using Chip8.Components;
using Chip8.Extensions;
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

    [Test]
    public void Chunk_ShouldCutAwayFromOriginalMemory()
    {
        // Arrange
        byte[] buffer = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04 };
        Memory<byte> memory = buffer;

        int chunk0Length = 2;
        int chunk1Length = 2;
        int chunk2Length = 1;
        int chunk3Length = 1;
        Memory<byte> chunk0 = memory.Chunk(chunk0Length);
        Memory<byte> chunk1 = memory.Chunk(chunk1Length);
        Memory<byte> chunk2 = memory.Chunk(chunk2Length);
        Memory<byte> chunk3 = chunk1[0..1];

        Assert.That(chunk0.Length, Is.EqualTo(chunk0Length));
        Assert.That(chunk1.Length, Is.EqualTo(chunk1Length));
        Assert.That(chunk2.Length, Is.EqualTo(chunk2Length));
        Assert.That(chunk3.Length, Is.EqualTo(chunk3Length));

        Assert.That(chunk0.Span[0], Is.EqualTo(buffer[0]));
        Assert.That(chunk0.Span[1], Is.EqualTo(buffer[1]));
        Assert.That(chunk1.Span[0], Is.EqualTo(buffer[2]));
        Assert.That(chunk1.Span[1], Is.EqualTo(buffer[3]));
        Assert.That(chunk2.Span[0], Is.EqualTo(buffer[4]));
        Assert.That(chunk3.Span[0], Is.EqualTo(buffer[2]));
    }
}