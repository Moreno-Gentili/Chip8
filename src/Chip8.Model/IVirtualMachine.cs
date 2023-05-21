using Chip8.Model.IO;

namespace Chip8.Model;

public interface IVirtualMachine
{
    static abstract IVirtualMachine Run(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker, IClock clock, int cyclesPerTick = 8);
    void SetSpeed(int cyclesPerTick);
    void Pause();
    void Resume();
    void Reset();
}
