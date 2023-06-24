using Chip8.Components.Base;
using Chip8.Model.Components;

namespace Chip8.Components;

public class Opcode : MemoryComponent
{
    public Opcode(Memory<byte> memory)
        : base (memory, MemorySize)
    {
    }

    public static int MemorySize => sizeof(ushort);

    public RegisterId X => (RegisterId)Nibble2;
    public RegisterId Y => (RegisterId)Nibble3;
    public byte N => Nibble4;
    public byte NN => Convert.ToByte((Nibble3 << 4) + Nibble4);
    public ushort NNN => Convert.ToUInt16((Nibble2 << 8) + NN);

    private byte Nibble1 => Convert.ToByte(memory.Span[0] >> 4);
    private byte Nibble2 => Convert.ToByte(memory.Span[0] & 0x0F);
    private byte Nibble3 => Convert.ToByte(memory.Span[1] >> 4);
    private byte Nibble4 => Convert.ToByte(memory.Span[1] & 0x0F);

    public static implicit operator Opcode(Memory<byte> memory)
    {
        return new Opcode(memory);
    }

    public void Deconstruct(out byte nibble1, out byte nibble2, out byte nibble3, out byte nibble4)
    {
        nibble1 = Nibble1;
        nibble2 = Nibble2;
        nibble3 = Nibble3;
        nibble4 = Nibble4;
    }
}