using Chip8.Components;

namespace Chip8;

public class VirtualMachine
{
    private readonly Memory memory = new Memory();
    private readonly Register[] registers = new Register[16];
    private readonly ICassette cassette;
    private readonly IMonitor monitor;
    private readonly ISpeaker speaker;

    public VirtualMachine(ICassette cassette, IMonitor monitor, ISpeaker speaker)
    {
        this.cassette = cassette;
        this.monitor = monitor;
        this.speaker = speaker;
    }

    public void Run()
    {
        
    }
}