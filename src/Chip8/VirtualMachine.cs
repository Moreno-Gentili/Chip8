using Chip8.Model.IO;
using Chip8.Components;

namespace Chip8;

public class VirtualMachine
{
    private readonly Memory memory = new Memory();
    private readonly Register[] registers = new Register[16];
    private readonly ICassette cassette;
    private readonly IDisplay display;
    private readonly ISpeaker speaker;

    public VirtualMachine(ICassette cassette, IDisplay display, ISpeaker speaker)
    {
        this.cassette = cassette;
        this.display = display;
        this.speaker = speaker;
    }

    public void Run()
    {
        
    }
}