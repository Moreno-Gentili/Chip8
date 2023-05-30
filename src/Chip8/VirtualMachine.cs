using Chip8.Model.IO;
using Chip8.Components;
using Chip8.Model;
using Chip8.Model.Components;

namespace Chip8;

public class VirtualMachine : IVirtualMachine
{
    // Components
    private IAddressableMemory addressableMemory;
    private IFrameBuffer frameBuffer;
    private IStack stack;
    private IRegisters registers;
    private ITimers timers;
    private IProcessor processor;

    // IO
    private readonly ICassette cassette;
    private readonly IKeyboard keyboard;
    private readonly IDisplay display;
    private readonly ISpeaker speaker;
    
    // Speed
    private int cyclesPerTick = 8;

    public VirtualMachine(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker, IClock clock)
    {
        // The original VM put the framebuffer, the stack, and the emulated registers at the top of accessible memory
        Memory<byte> memory = new byte[AddressableMemory.MemorySize + FrameBuffer.MemorySize + Stack.MemorySize + Registers.MemorySize + Timers.MemorySize];
        addressableMemory = AddressableMemory.From(memory);
        frameBuffer = FrameBuffer.From(memory);
        stack = Stack.From(memory);
        registers = Registers.From(memory);
        timers = Timers.From(memory, clock, speaker);
        
        processor = new Processor();

        this.cassette = cassette;
        this.keyboard = keyboard;
        this.display = display;
        this.speaker = speaker;

        clock.Tick += ExecuteCycles;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public static IVirtualMachine Run(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker, IClock clock, int cyclesPerTick = 8)
    {
        VirtualMachine vm = new(cassette, keyboard, display, speaker, clock);
        vm.SetSpeed(cyclesPerTick);
        return vm;
    }

    public void SetSpeed(int cyclesPerTick)
    {
        if (cyclesPerTick <= 0)
        {
            throw new ArgumentException("Speed must be greater than 0", nameof(cyclesPerTick));
        }

        this.cyclesPerTick = cyclesPerTick;
    }

    private void ExecuteCycles(object? sender, TimeSpan elapsed)
    {
        bool shouldRedraw = true;
        for (int i = 0; i < cyclesPerTick; i++)
        {
            IOpcode? opcode = processor.ExecuteCycle(addressableMemory, frameBuffer, stack, registers, timers, keyboard, speaker);
            if (opcode is null)
            {
                break;
            }

            shouldRedraw = true;
        }

        if (shouldRedraw)
        {

            frameBuffer.Draw(new Sprites.DefaultFont().Digit9, 0, 0);
            bool zero = frameBuffer[0, 0];
            display.Show(frameBuffer);
        }
    }
}