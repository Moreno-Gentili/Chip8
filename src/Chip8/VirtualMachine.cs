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
    private readonly Clock clock;
    private readonly Memory<byte> memory;
    private readonly IAddressableMemory addressableMemory;
    private readonly IFrameBuffer frameBuffer;
    private readonly IStack stack;
    private readonly IRegisters registers;
    private readonly ITimers timers;
    private readonly IProcessor processor;
    private readonly IFont font;

    // IO
    private readonly ICassette cassette;
    private readonly IKeyboard keyboard;
    private readonly IDisplay display;

    private VirtualMachine(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker)
    {
        clock = new Clock();

        // The original VM put the framebuffer, the stack, and the emulated registers at the top of accessible memory
        memory = new byte[AddressableMemory.MemorySize + FrameBuffer.MemorySize + Stack.MemorySize + Registers.MemorySize + Timers.MemorySize];
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
    }

    public void Reset()
    {
        Clear(memory);
        LoadFont(font, registers, addressableMemory);   
        LoadProgram(cassette, registers, addressableMemory);
    }

    public static IVirtualMachine Create(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker)
    {
        return new VirtualMachine(cassette, keyboard, display, speaker);
    }

    public int CpuInstructionsPerSecond
    {
        get
        {
            return clock.CpuInstructionsPerSecond;
        }

        set
        {
            clock.CpuInstructionsPerSecond = value;
        }
    }

    public void Update(TimeSpan time)
    {
        bool shouldRedraw = false;
        int instructionsCount = clock.Update(time);

        for (int i = 0; i < instructionsCount; i++, shouldRedraw = true)
        {
            ProgramCounterHint result = processor.ExecuteOpcode(addressableMemory, frameBuffer, stack, registers, timers, font, keyboard);
            if (result == ProgramCounterHint.WaitForKey)
            {
                break;
            }
        }

        if (shouldRedraw)
        {
            display.Show(frameBuffer);
        }
    }

    private static void Clear(Memory<byte> memory)
    {
        for (int i = 0; i < memory.Length; i++)
        {
            memory.Span[i] = byte.MinValue;
        }
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
}