namespace Chip8.Components;

public class Clock : IClock
{
    private const int hertz = 60;
    private readonly Timer timer;
    public event TickDelegate? Tick;

    public Clock()
    {
        timer = new Timer(RaiseTick, null, TimeSpan.Zero, TimeSpan.FromSeconds(1) / hertz);
    }

    private void RaiseTick(object? state)
    {
        System.Threading.Thread.Sleep()
        Tick?.Invoke();
    }
}

public delegate void TickDelegate();

public interface IClock
{
    event TickDelegate? Tick;
}