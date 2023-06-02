using Chip8.Components.Base;
using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Components;

public class AddressableMemory : MemoryComponent, IAddressableMemory
{
    public AddressableMemory(Memory<byte> memory)
        : base(memory, MemorySize)
    {
    }

    public static int MemorySize = 0x1000;

    Range IAddressableMemory.FontRange => 0x000..0x200;
    Range IAddressableMemory.ProgramRange => 0x200..MemorySize;

    public Memory<byte> Read(IRegister<ushort> position, ushort length)
    {
        return memory.Slice(position.GetValue(), length);
    }

    internal static AddressableMemory AllocateFrom(ref Memory<byte> memory)
    {
        return new AddressableMemory(memory.Chunk(MemorySize));
    }

    public void Write(IRegister<ushort> position, Memory<byte> data)
    {
        data.CopyTo(memory[position.GetValue()..]);
    }
}