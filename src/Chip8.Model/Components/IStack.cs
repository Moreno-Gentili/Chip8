namespace Chip8.Model.Components;

// The original RCA 1802 version allocated 48 bytes for up to 12 levels of nesting
public interface IStack
{
    void Push(byte level, ushort address);
}
