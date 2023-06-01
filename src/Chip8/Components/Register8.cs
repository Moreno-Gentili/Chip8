using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Components;

public class Register8 : MemoryComponent, IRegisterV, IStackPointer
{
    public Register8(Memory<byte> memory)
        : base(memory, MemorySize)
    {
    }

    public static int MemorySize => sizeof(byte);

    public static Register8 From(Memory<byte> memory)
    {
        return new Register8(memory.Chunk(MemorySize));
    }

    public byte GetValue()
    {
        return memory.Span[0];
    }

    public void SetValue(byte value)
    {
        memory.Span[0] = value;
    }
}

