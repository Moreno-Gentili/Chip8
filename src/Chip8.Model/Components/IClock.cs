namespace Chip8.Model.Components;

public interface IClock
{
    event EventHandler<TimeSpan> Tick;
}