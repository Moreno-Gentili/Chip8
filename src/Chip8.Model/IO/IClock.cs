namespace Chip8.Model.IO;

public interface IClock
{
    event EventHandler<TimeSpan> Tick;
}