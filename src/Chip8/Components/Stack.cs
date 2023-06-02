using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Components;

public class Stack : MemoryComponent, IStack
{
    public Stack(Memory<byte> memory)
        : base(memory, MemorySize)
    {
    }

    public static int MemorySize = 16 * sizeof(ushort);

    internal static Stack AllocateFrom(ref Memory<byte> memory)
    {
        return new Stack(memory.Chunk(MemorySize));
    }

    public void SetAddress(IStackPointer stackPointer, ushort address)
    {
        byte position = Convert.ToByte(stackPointer.GetValue() * sizeof(ushort));
        BitConverter.GetBytes(address).CopyTo(memory[position..]);
    }

    public ushort GetAddress(IStackPointer stackPointer)
    {
        byte position = Convert.ToByte(stackPointer.GetValue() * sizeof(ushort));
        return BitConverter.ToUInt16(memory[position..(position + sizeof(ushort))].Span);
    }
}
