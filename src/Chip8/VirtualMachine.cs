using Chip8.Model.IO;
using Chip8.Components;
using Chip8.Model;
using Chip8.Model.Components;
using Chip8.Model.Sprites;
using Chip8.Sprites;
using Chip8.Extensions;

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
    private IFont font;

    // IO
    private readonly ICassette cassette;
    private readonly IKeyboard keyboard;
    private readonly IDisplay display;
    
    // Speed
    private int cyclesPerTick = 8;

    public VirtualMachine(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker, IClock clock)
    {
        // The original VM put the framebuffer, the stack, and the emulated registers at the top of accessible memory
        Memory<byte> memory = new byte[AddressableMemory.MemorySize + FrameBuffer.MemorySize + Stack.MemorySize + Registers.MemorySize + Timers.MemorySize];
        addressableMemory = AddressableMemory.AllocateFrom(ref memory);
        frameBuffer = FrameBuffer.AllocateFrom(ref memory);
        stack = Stack.AllocateFrom(ref memory);
        registers = Registers.AllocateFrom(ref memory);
        timers = Timers.AllocateFrom(ref memory, clock, speaker);
        font = new DefaultFont();
        
        processor = new Processor();

        this.cassette = cassette;
        this.keyboard = keyboard;
        this.display = display;

        Reset();

        clock.Tick += ExecuteCycles;
    }

    public void Reset()
    {
        LoadFont(font, registers, addressableMemory);   
        LoadProgram(cassette, registers, addressableMemory);
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

    private static void LoadProgram(ICassette cassette, IRegisters registers, IAddressableMemory addressableMemory)
    {
        ushort programLocation = Convert.ToUInt16(addressableMemory.ProgramRange.Start.Value);
        registers.ProgramCounter.SetValue(programLocation);
        registers.StackPointer.SetValue(0);

        Memory<byte> program = cassette.Load();
        addressableMemory.Write(registers.ProgramCounter, program);
    }

    private static void LoadFont(IFont font, IRegisters registers, IAddressableMemory addressableMemory)
    {
        ushort fontLocation = Convert.ToUInt16(addressableMemory.FontRange.Start.Value);
        registers.ProgramCounter.SetValue(fontLocation);

        List<FontDigit> digits = Enum.GetValues<FontDigit>().ToList();
        for (int i = 0; i < digits.Count; i++)
        {
            addressableMemory.WriteSprite(font.Digits[(FontDigit)i], registers.ProgramCounter);
        }
    }

    private void ExecuteCycles(object? sender, TimeSpan elapsed)
    {
        bool shouldRedraw = false;

        for (int i = 0; i < cyclesPerTick; i++, shouldRedraw = true)
        {
            ExecuteResult result = processor.ExecuteOpcode(addressableMemory, frameBuffer, stack, registers, timers, font, keyboard);
            if (result == ExecuteResult.WaitingForKey)
            {
                break;
            }
        }

        if (shouldRedraw)
        {
            display.Show(frameBuffer);
        }
    }
}