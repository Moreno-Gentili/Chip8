using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Components;

public class SoundTimer : ITimer
{
    private const int Length = 1;
    private readonly Memory<byte> buffer;
    private readonly IClock clock;
    private readonly ISpeaker speaker;

    public SoundTimer(Memory<byte> buffer, IClock clock, ISpeaker speaker)
    {
        if (buffer.Length > 1)
        {
            throw new ArgumentException($"Length must be {Length}", nameof(buffer));
        }

        this.clock = clock;
        this.buffer = buffer;
        this.speaker = speaker;
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

    private void Decrement(object? sender, TimeSpan elapsed)
    {
        SetValue(Convert.ToByte(buffer.Span[0] - 1));
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
