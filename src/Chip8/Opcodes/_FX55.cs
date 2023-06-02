using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX55
{
    public static ExecuteResult Execute(IRegisters registers, IAddressableMemory addressableMemory, byte x)
    {
        Memory<byte> memory = registers.V[..(x + 1)];
        addressableMemory.Write(registers.I, memory);

        return ExecuteResult.Proceed;
    }
}
