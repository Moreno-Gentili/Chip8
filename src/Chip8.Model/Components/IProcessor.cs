using Chip8.Model.IO;
using Chip8.Model.Sprites;

namespace Chip8.Model.Components;

public interface IProcessor
{
    // Each CHIP-8 instruction took a variable number of clock cycles to complete.
    ProgramCounterResult ExecuteOpcode(IAddressableMemory Memory, IFrameBuffer frameBuffer, IStack stack, IRegisters Registers, ITimers timers, IFont font, IKeyboard keyboard);
}

public enum ProgramCounterResult
{
    Advance,
    Stay,
    SkipOneThenAdvance,
    WaitingForKey
}
