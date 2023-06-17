using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX55
{
    public static ProgramCounterHint Execute(IRegisters registers, IAddressableMemory addressableMemory, RegisterId x)
    {
        Memory<byte> memory = registers.V[RegisterId.V0, x];
        addressableMemory.Write(registers.I, memory);

        return ProgramCounterHint.Advance;
    }
}
