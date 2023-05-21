using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Components;

// The delay timer is active whenever the DT register is non-zero, when active it will keep decrementing itself at a rate of 60 Hz until it is zero again.
// When zero, it will deactivate itself, not decrementing anymore.
public class DelayTimer : ITimer
{
    private const int Length = 1;
    private readonly Memory<byte> buffer;
    private readonly IClock clock;
    public DelayTimer(Memory<byte> buffer, IClock clock)
    {
        if (buffer.Length > 1)
        {
            throw new ArgumentException($"Length must be {Length}", nameof(buffer));
        }

        this.clock = clock;
        this.buffer = buffer;
    }

    public byte GetValue()
    {
        return buffer.Span[0];
    }

    public void SetValue(byte value)
    {
        if (value > 0)
        {
            if (buffer.Span[0] == 0)
            {
                Activate();
            }

            buffer.Span[0] = value;
        }
        else
        {
            if (buffer.Span[0] > 0)
            {
                Deactivate();
            }

            buffer.Span[0] = value;
        }
    }

    private void Activate()
    {
        clock.Tick += Decrement;
    }

    private void Deactivate()
    {
        clock.Tick -= Decrement;
    }

    private void Decrement(object? sender, TimeSpan elapsed)
    {
        SetValue(Convert.ToByte(buffer.Span[0] - 1));
    }
}
