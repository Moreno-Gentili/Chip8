using Chip8.Model.IO;
using Chip8.Model.Sprites;

namespace Chip8.Model.Components;

public interface IProcessor
{
    // Each CHIP-8 instruction took a variable number of clock cycles to complete.
    ProgramCounterHint ExecuteOpcode(IAddressableMemory Memory, IFrameBuffer frameBuffer, IStack stack, IRegisters Registers, ITimers timers, IFont font, IKeyboard keyboard);
}

public enum ProgramCounterHint
{
    Advance,
    Stay,
    SkipOneThenAdvance,
    WaitForKey,
    Faulted
}
