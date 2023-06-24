using Chip8.Model.Components;

namespace Chip8.Components;

internal class Clock : IClock
{
    private int ticksPerSecond = 60;
    private int cpuInstructionsPerSecond = 600;
    private TimeSpan tickLastUpdate = TimeSpan.Zero;
    private TimeSpan cpuLastUpdate = TimeSpan.Zero;

    public event EventHandler<TimeSpan>? Tick;

    public int TicksPerSecond
    {
        get
        {
            return ticksPerSecond;
        }

        set
        {
            if (ticksPerSecond <= 0)
            {
                throw new ArgumentException($"Value must be greater than 0");
            }

            ticksPerSecond = value;
        }
    }

    public int CpuInstructionsPerSecond
    {
        get
        {
            return cpuInstructionsPerSecond;
        }

        set
        {
            if (cpuInstructionsPerSecond <= 0)
            {
                throw new ArgumentException($"Value must be greater than 0");
            }

            cpuInstructionsPerSecond = value;
        }
    }

    public TimeSpan LastUpdate => cpuLastUpdate;

    public void Reset()
    {
        tickLastUpdate = TimeSpan.Zero;
        cpuLastUpdate = TimeSpan.Zero;
    }

    public int Update(TimeSpan time)
    {
        TickIfNeeded(time);   
        return GetInstructionsCount(time);
    }

    private void TickIfNeeded(TimeSpan time)
    {
        if (tickLastUpdate == TimeSpan.Zero)
        {
            tickLastUpdate = time;
            return;
        }

        TimeSpan tickInterval = TimeSpan.FromMilliseconds(1000 / (double)ticksPerSecond);
        while ((time - tickLastUpdate) >= tickInterval)
        {
            tickLastUpdate += tickInterval;
            Tick?.Invoke(this, tickLastUpdate);
        }
    }

    private int GetInstructionsCount(TimeSpan time)
    {
        if (cpuLastUpdate == TimeSpan.Zero)
        {
            cpuLastUpdate = time;
            return 0;
        }

        TimeSpan difference = time - cpuLastUpdate;
        double ratio = difference / TimeSpan.FromSeconds(1);
        int instructionsCount = Convert.ToInt32(Math.Floor(ratio * cpuInstructionsPerSecond));
        TimeSpan actualIncrement = (instructionsCount / (double)cpuInstructionsPerSecond) * TimeSpan.FromSeconds(1);
        cpuLastUpdate += actualIncrement;
        return instructionsCount;
    }
}
