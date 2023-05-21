namespace Chip8.Model.Components;

public interface ITimers
{
    ITimer SoundTimer { get; }
    ITimer DelayTimer { get; }
}
