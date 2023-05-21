using Chip8.Model.IO;
using Chip8.Components;
using Chip8.Model;
using PrecisionTiming;
using Chip8.Model.Components;

namespace Chip8;

public class VirtualMachine : IVirtualMachine, IRegisters, ITimers
{
    // The original VM put the framebuffer, the stack, and the emulated registers at the top of accessible memory

    private readonly PrecisionTimer timer;
    private byte[] memory = new byte[4096];
    private int cyclesPerTick = 8;
    private FrameBuffer frameBuffer;
    private Stack stack;
    private IRegister<byte>[] registerVx;
    private IRegister<ushort> registerI;

    private ICassette? cassette;
    private IKeyboard? keyboard;
    private IDisplay? display;
    private ISpeaker? speaker;

    static VirtualMachine()
    {
        PrecisionTimerSettings.SetMinimumTimerResolution(0);
    }

    public VirtualMachine()
    {
        stack = new Stack();
        timer = CreateTimer();
    }

    public ITimer SoundTimer => throw new NotImplementedException();

    public ITimer DelayTimer => throw new NotImplementedException();

    public IRegister<ushort> GetI()
    {
        throw new NotImplementedException();
    }

    public IRegister<byte> GetVx(RegisterVx name)
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        if (timer.IsRunning())
        {
            timer.Stop();
        }
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Resume()
    {
        if (!timer.IsRunning())
        {
            timer.Start();
        }
    }

    public void Run(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker)
    {
        this.cassette = cassette;
        this.keyboard = keyboard;
        this.display = display;
        this.speaker = speaker;
        Resume();
    }

    public void SetSpeed(int cyclesPerTick)
    {
        if (cyclesPerTick <= 0)
        {
            throw new ArgumentException("Speed must be greater than 0", nameof(cyclesPerTick));
        }

        this.cyclesPerTick = cyclesPerTick;
    }

    private PrecisionTimer CreateTimer()
    {
        PrecisionTimer timer = new();
        timer.SetAction(ExecuteCycles);
        timer.SetPeriod(1000 / 60);
        timer.SetAutoResetMode(true);

        return timer;
    }

    private void ExecuteCycles()
    {

    }
}