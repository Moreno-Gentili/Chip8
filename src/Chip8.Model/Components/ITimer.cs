namespace Chip8.Model.Components;

public interface ITimer
{
    byte GetValue();
    void SetValue(byte value);
}