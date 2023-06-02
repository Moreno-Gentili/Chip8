using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Components;

// The delay timer is active whenever the DT register is non-zero, when active it will keep decrementing itself at a rate of 60 Hz until it is zero again.
// When zero, it will deactivate itself, not decrementing anymore.
public class DelayTimer : MemoryComponent, ITimer
{
    private readonly IClock clock;

    public DelayTimer(Memory<byte> memory, IClock clock)
        : base(memory, MemorySize)
    {
        this.clock = clock;
    }

    public static int MemorySize = sizeof(byte);

    internal static ITimer AllocateFrom(ref Memory<byte> memory, IClock clock)
    {
        return new DelayTimer(memory.Chunk(MemorySize), clock);
    }

    public byte GetValue()
    {
        return memory.Span[0];
    }

    public void SetValue(byte value)
    {
        if (value > 0)
        {
            if (memory.Span[0] == 0)
            {
                Activate();
            }

            memory.Span[0] = value;
        }
        else
        {
            if (memory.Span[0] > 0)
            {
                Deactivate();
            }

            memory.Span[0] = value;
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
        if (memory.Span[0] > 0)
        {
            SetValue(Convert.ToByte(memory.Span[0] - 1));
        }
    }
}
