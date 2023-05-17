namespace Chip8.IO;

// There is only one tone on the Chip-8, decided by the implementor.
public interface ISound
{
    void ActivateBeep();
    void DeactivateBeep();
}