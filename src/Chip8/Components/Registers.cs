using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Components;

public class Registers : MemoryComponent, IRegisters
{
    private readonly IRegisterI i;
    private readonly IRegisterVCollection v;
    private readonly IProgramCounter programCounter;
    private readonly IStackPointer stackPointer;

    public Registers(Memory<byte> memory)
        : base(memory, MemorySize)
    {
        i = Register16.AllocateFrom(ref memory);
        v = RegisterVCollection.AllocateFrom(ref memory, VxCount);
        programCounter = Register16.AllocateFrom(ref memory);
        stackPointer = Register8.AllocateFrom(ref memory);
    }

    public static int MemorySize => VxSize + ISize + PcSize + SpSize;
    private static int VxCount => 16;
    private static int VxSize => VxCount * Register8.MemorySize;
    private static int ISize => 1 * Register16.MemorySize;
    private static int PcSize = 1 * Register16.MemorySize;
    private static int SpSize = 1 * Register8.MemorySize;

    public static Registers AllocateFrom(ref Memory<byte> memory)
    {
        return new Registers(memory.Chunk(MemorySize));
    }

    public IRegisterVCollection V => v;
    public IRegisterI I => i;
    public IProgramCounter ProgramCounter => programCounter;
    public IStackPointer StackPointer => stackPointer;
}

public class RegisterVCollection : MemoryComponent, IRegisterVCollection
{
    private readonly IRegisterV[] v;

    private RegisterVCollection(Memory<byte> memory)
        : base(memory, memory.Length / MemorySize)
    {
        v = Enumerable.Range(0, memory.Length / MemorySize).Select(i => (IRegisterV)Register8.AllocateFrom(ref memory)).ToArray();
    }

    public IRegisterV this[RegisterName registerName] => v[(int) registerName];

    public Memory<byte> this[RegisterName fromRegisterName, RegisterName toRegisterName]
    {
        get
        {
            byte lowerInclusiveBound = Convert.ToByte(fromRegisterName);
            byte upperExclusiveBound = Convert.ToByte(toRegisterName + 1);
            return memory[lowerInclusiveBound..upperExclusiveBound];
        }

        set
        {
            value.CopyTo(memory);
        }
    }

    public static int MemorySize => sizeof(byte);

    internal static RegisterVCollection AllocateFrom(ref Memory<byte> memory, int count)
    {
        return new RegisterVCollection(memory.Chunk(MemorySize * count));
    }
}
