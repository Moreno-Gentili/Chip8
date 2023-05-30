using Chip8.Model.IO;
using System.Diagnostics;
using System.Windows.Threading;

namespace Chip8.Wpf.IO;

public class Clock : IClock
{
    private readonly DispatcherTimer timer;
    private readonly Stopwatch stopwatch;

    public Clock(int fps = 60)
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        timer = new DispatcherTimer(TimeSpan.FromMilliseconds(1000 / (double)fps), DispatcherPriority.Normal, RaiseTick, Dispatcher.CurrentDispatcher);
        timer.Start();
    }

    public event EventHandler<TimeSpan>? Tick;

    private void RaiseTick(object? sender, EventArgs e)
    {
        Tick?.Invoke(this, stopwatch.Elapsed);
    }

}
