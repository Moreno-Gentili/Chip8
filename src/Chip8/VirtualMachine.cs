using Chip8.Model.IO;
using Chip8.Components;
using Chip8.Model;
using Haukcode.HighResolutionTimer;

namespace Chip8;

public class VirtualMachine : IVirtualMachine
{
    // The original VM put the framebuffer, the stack, and the emulated registers at the top of accessible memory

    private readonly HighResolutionTimer timer;
    private byte[] memory = new byte[4096];
    private int cyclesPerTick = 8;

    public VirtualMachine()
    {
        timer = new HighResolutionTimer();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Resume()
    {
        throw new NotImplementedException();
    }

    public void Run()
    {
        
    }

    public void Run(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker)
    {
        throw new NotImplementedException();
    }

    public void SetSpeed(int cyclesPerTick)
    {
        throw new NotImplementedException();
    }
}