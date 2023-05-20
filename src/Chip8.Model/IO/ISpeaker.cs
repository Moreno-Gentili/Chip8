namespace Chip8.Model.IO;

// There is only one tone on the Chip-8, decided by the implementor.
public interface ISpeaker
{
    void SetBuzzer(bool on);
}