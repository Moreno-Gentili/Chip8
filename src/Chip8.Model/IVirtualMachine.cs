using Chip8.Model.IO;

namespace Chip8.Model;

public interface IVirtualMachine
{
    void Run(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker);
    void SetSpeed(int cyclesPerTick);
    void Pause();
    void Resume();
    void Reset();
}
