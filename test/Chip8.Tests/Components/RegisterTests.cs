using Chip8.Components;
using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.Sprites;
using Chip8.Sprites;

namespace Chip8.Tests;

public class RegisterTests
{
    [Test]
    public void SetValue_ShouldRoundtrip_When8BitRegister()
    {
        // Arrange
        Memory<byte> memory = new byte[] { 0x00 };
        IRegister<byte> register = Register8.AllocateFrom(ref memory);
        byte expectedValue = 0x80;

        // Act
        register.SetValue(expectedValue);

        // Assert
        Assert.That(expectedValue, Is.EqualTo(register.GetValue()));
    }

    [Test]
    public void SetValue_ShouldRoundtrip_When16BitRegister()
    {
        // Arrange
        Memory<byte> memory = new byte[] { 0x00, 0x00 };
        IRegister<ushort> register = Register16.AllocateFrom(ref memory);
        ushort expectedValue = 0x200;

        // Act
        register.SetValue(expectedValue);

        // Assert
        Assert.That(expectedValue, Is.EqualTo(register.GetValue()));
    }

    [Test]
    public void Increment_ShouldRoundtrip_WhenProgramCounter()
    {
        // Arrange
        Memory<byte> memory = new byte[] { 0x00, 0x00 };
        IProgramCounter register = Register16.AllocateFrom(ref memory);
        ushort expectedValue = 0x200;
        ushort increment = 3;

        // Act
        register.SetValue(expectedValue);
        register.Increment(increment);

        // Assert
        Assert.That(expectedValue + increment, Is.EqualTo(register.GetValue()));
    }
}