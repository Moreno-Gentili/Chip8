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

    internal static Stack From(Memory<byte> memory)
    {
        return new Stack(memory.Chunk(MemorySize));
    }

    public void Push(byte level, ushort address)
    {
        throw new NotImplementedException();
    }
}
