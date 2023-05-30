using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Components;

public class Registers : MemoryComponent, IRegisters
{
    private readonly IRegister<ushort> i;
    private readonly IRegister<byte>[] vx;
    private readonly IRegister<ushort> programCounter;
    private readonly IRegister<byte> stackPointer;

    public Registers(Memory<byte> memory)
        : base(memory, MemorySize)
    {
        i = Register16.From(memory);
        vx = Enumerable.Range(0, VxCount).Select(i => Register8.From(memory)).ToArray();
        programCounter = Register16.From(memory);
        stackPointer = Register8.From(memory);
    }

    public static int MemorySize => VxSize + ISize + PcSize + SpSize;
    private static int VxCount => 16;
    private static int VxSize => VxCount * Register8.MemorySize;
    private static int ISize => 1 * Register16.MemorySize;
    private static int PcSize = 1 * Register16.MemorySize;
    private static int SpSize = 1 * Register8.MemorySize;

    public static Registers From(Memory<byte> memory)
    {
        return new Registers(memory.Chunk(MemorySize));
    }

    public IRegister<byte> GetVx(RegisterVx name) => vx[(int)name];
    public IRegister<ushort> GetI() => i;
    public IRegister<ushort> GetProgramCounter() => programCounter;
    public IRegister<byte> GetStackPointer() => stackPointer;
}

