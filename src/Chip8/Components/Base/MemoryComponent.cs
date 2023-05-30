namespace Chip8.Components.Base;

public abstract class MemoryComponent
{
    protected readonly Memory<byte> memory;
    public MemoryComponent(Memory<byte> memory, int memorySize)
    {
        if (memory.Length != memorySize)
        {
            throw new ArgumentException($"Component {GetType().Name} requires a memory buffer of size {memorySize}");
        }

        this.memory = memory;
    }
}
