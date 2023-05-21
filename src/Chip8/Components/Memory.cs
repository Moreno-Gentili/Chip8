using Chip8.Model.Components;

namespace Chip8.Components;

public class Memory : IMemory
{
    private readonly Memory<byte> buffer;

    public Memory(Memory<byte> buffer)
    {
        this.buffer = buffer;
    }

    public Memory<byte> Read(ushort offset, ushort length)
    {
        return buffer.Slice(offset, length);
    }

    public void Write(ushort offset, Memory<byte> data)
    {
        data.CopyTo(buffer[offset..]);
    }
}