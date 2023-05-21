using Chip8.Model.IO;
using Chip8.Components;
using Chip8.Model;
using Chip8.Model.Components;

namespace Chip8;

public class VirtualMachine : IVirtualMachine, IRegisters, ITimers
{
    // Components
    private IMemory memory;
    private IFrameBuffer frameBuffer;
    private IStack stack;
    private ITimer delayTimer;
    private ITimer soundTimer;
    private IRegister<byte>[] registersVx;
    private IRegister<ushort> registerI;
    private IProcessor processor;

    // IO
    private readonly ICassette cassette;
    private readonly IKeyboard keyboard;
    private readonly IDisplay display;
    private readonly ISpeaker speaker;
    private readonly IClock clock;
    
    // Speed
    private int cyclesPerTick = 8;

    public VirtualMachine(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker, IClock clock)
    {
        int accessibleMemorySize = 4 * 1024, // 4kB buffer
            frameBufferSize = 64 * 32 / 8,   // 64 by 32 pixels, 1 bit per pixel
            stackSize = 16 * 2,              // 16 nested levels of 16 bits each 
            registersVxSize = 16 * 1,        // 16 Vx registers, 1 byte each
            registerISize = 1 * 2,           // 1 I register, 2 bytes
            programCounterSize = 1 * 2,      // 1 pseudo register, 2 bytes
            stackPointerSize = 1 * 1;        // 1 pointer, 1 byte

    // The original VM put the framebuffer, the stack, and the emulated registers at the top of accessible memory
    Memory<byte> buffer = new byte[accessibleMemorySize + frameBufferSize + stackSize + registersVxSize + registerISize + programCounterSize + stackPointerSize];
    memory = new Memory(Slice(buffer, accessibleMemorySize));
    
    frameBuffer = new FrameBuffer(memory.Slice(position, frameBufferSize));

    position += frameBufferSize;
    stack = new Stack(memory.Slice(position += FrameBufferSize, StackSize));

        Enumerable.Range(0, RegistersS).Select(r => new Register<byte>(memory.Slice(position + (int)r))).ToArray();
        
        registers = this;
        timers = this;
    }

    private Memory<byte> Slice(ref Memory<byte> buffer, int length)
    {
        Memory<byte> slice = buffer.Slice(0, length);
        buffer = buffer.Slice(length);
        return slice;
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

    public static IVirtualMachine Run(ICassette cassette, IKeyboard keyboard, IDisplay display, ISpeaker speaker, IClock clock, int cyclesPerTick = 8)
    {
        VirtualMachine vm = new(cassette, keyboard, display, speaker, clock);
        vm.SetSpeed(cyclesPerTick);
        vm.Resume();
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
        IOpcode? opcode = null;
        do
        {
            opcode = processor.ExecuteCycle(memory, frameBuffer, stack, registers, timers, keyboard, speaker)
        }
    }
}