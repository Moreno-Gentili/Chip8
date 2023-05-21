using Chip8.Model.Components;

namespace Chip8.Components;

public class Stack : IStack
{
    private const int Length = 16 * 2;
    private readonly Memory<byte> buffer;

    public Stack(Memory<byte> buffer)
    {
        if (buffer.Length != Length)
        {
            throw new ArgumentException($"Length must be {Length}", nameof(buffer));
        }

        this.buffer = buffer;
    }

    public void Push(byte level, ushort address)
    {
        throw new NotImplementedException();
    }
}
