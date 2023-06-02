using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Components;

public class SoundTimer : MemoryComponent, ITimer
{
    private readonly IClock clock;
    private readonly ISpeaker speaker;

    public SoundTimer(Memory<byte> memory, IClock clock, ISpeaker speaker)
        : base(memory, MemorySize)
    {
        this.clock = clock;
        this.speaker = speaker;
    }

    public static int MemorySize = sizeof(byte);

    internal static ITimer AllocateFrom(ref Memory<byte> memory, IClock clock, ISpeaker speaker)
    {
        return new SoundTimer(memory.Chunk(MemorySize), clock, speaker);
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

    private void Decrement(object? sender, TimeSpan elapsed)
    {
        if (memory.Span[0] > 0)
        {
            SetValue(Convert.ToByte(memory.Span[0] - 1));
        }
    }

    private void Activate()
    {
        clock.Tick += Decrement;
        speaker.SetBuzzer(true);
    }

    private void Deactivate()
    {
        clock.Tick -= Decrement;
        speaker.SetBuzzer(false);
    }
}
