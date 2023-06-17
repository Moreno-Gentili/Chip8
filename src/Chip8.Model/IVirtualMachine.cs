using Chip8.Model.IO;

namespace Chip8.Model;

public interface IVirtualMachine
{
    static abstract IVirtualMachine Create(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker);
    int CpuInstructionsPerSecond { get; set; }
    void Reset();
    Task Cycle(TimeSpan time);
}
