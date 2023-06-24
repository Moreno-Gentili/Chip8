using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Components;

public class Register16 : MemoryComponent, IRegisterI, IProgramCounter
{
    public Register16(Memory<byte> memory)
        : base(memory, MemorySize)
    {
    }

    public static int MemorySize => sizeof(ushort);

    public static Register16 AllocateFrom(ref Memory<byte> memory)
    {
        return new Register16(memory.Chunk(MemorySize));
    }

    public ushort GetValue()
    {
        return Convert.ToUInt16((memory.Span[0] << 8) + memory.Span[1]);
    }

    public void SetValue(ushort value)
    {
        memory.Span[0] = Convert.ToByte(value >> 8);
        memory.Span[1] = Convert.ToByte(value & 0xFF);
    }
}

