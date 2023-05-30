using Chip8.Components;
using Chip8.Model.Components;
using Chip8.Model.Sprites;
using Chip8.Sprites;

namespace Chip8.Tests;

public class SpriteTests
{
    [Test]
    public void Indexer_ShouldReturnTrueForLitPixels()
    {
        // Arrange
        ISprite digit0 = new DefaultFont().Digit0;

        // Assert
        Assert.IsTrue(digit0[0, 0]);
        Assert.IsTrue(digit0[1, 0]);
        Assert.IsTrue(digit0[2, 0]);
        Assert.IsTrue(digit0[3, 0]);

        Assert.IsTrue(digit0[0, 1]);
        Assert.IsFalse(digit0[1, 1]);
        Assert.IsFalse(digit0[2, 1]);
        Assert.IsTrue(digit0[3, 1]);

        Assert.IsTrue(digit0[0, 2]);
        Assert.IsFalse(digit0[1, 2]);
        Assert.IsFalse(digit0[2, 2]);
        Assert.IsTrue(digit0[3, 2]);

        Assert.IsTrue(digit0[0, 3]);
        Assert.IsFalse(digit0[1, 3]);
        Assert.IsFalse(digit0[2, 3]);
        Assert.IsTrue(digit0[3, 3]);

        Assert.IsTrue(digit0[0, 4]);
        Assert.IsTrue(digit0[1, 4]);
        Assert.IsTrue(digit0[2, 4]);
        Assert.IsTrue(digit0[3, 4]);
    }
}