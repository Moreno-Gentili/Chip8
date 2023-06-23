using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;
using System.Text.RegularExpressions;

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
        byte offset = Convert.ToByte(stackPointer.GetValue() * sizeof(ushort));
        BitConverter.GetBytes(address).CopyTo(memory[offset..]);
    }

    public ushort GetAddress(IStackPointer stackPointer)
    {
        byte offset = Convert.ToByte(stackPointer.GetValue() * sizeof(ushort));
        return BitConverter.ToUInt16(memory[offset..(offset + sizeof(ushort))].Span);
    }

    public string ToString(IStackPointer stackPointer)
    {
        string value = base.ToString()[0..(stackPointer.GetValue() * sizeof(ushort) * 2)];
        return Regex.Replace(value, ".{4}", "$0 ");
    }
}
