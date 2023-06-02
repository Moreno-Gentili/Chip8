using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Components;

public class Timers : MemoryComponent, ITimers
{
    private readonly ITimer soundTimer;
    private readonly ITimer delayTimer;

    public Timers(Memory<byte> memory, IClock clock, ISpeaker speaker)
        : base(memory, MemorySize)
    {
        this.soundTimer = SoundTimer.AllocateFrom(ref memory, clock, speaker);
        this.delayTimer = DelayTimer.AllocateFrom(ref memory, clock);
    }

    public static int MemorySize = SoundTimer.MemorySize + DelayTimer.MemorySize;
    ITimer ITimers.SoundTimer => soundTimer;
    ITimer ITimers.DelayTimer => delayTimer;

    public static Timers AllocateFrom(ref Memory<byte> memory, IClock clock, ISpeaker speaker)
    {
        return new Timers(memory.Chunk(MemorySize), clock, speaker);
    }
    
}
