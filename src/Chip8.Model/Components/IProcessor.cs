using Chip8.Model.IO;

namespace Chip8.Model.Components;

public interface IProcessor
{
    // Each CHIP-8 instruction took a variable number of clock cycles to complete.
    IOpcode? ExecuteCycle(IMemory Memory, IFrameBuffer frameBuffer, IStack stack, IRegisters Registers, ITimers timers, IKeyboard keyboard, ISpeaker speaker);
}
