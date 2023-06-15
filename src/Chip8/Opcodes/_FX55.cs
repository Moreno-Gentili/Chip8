using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX55
{
    public static ProgramCounterResult Execute(IRegisters registers, IAddressableMemory addressableMemory, RegisterName x)
    {
        Memory<byte> memory = registers.V[RegisterName.V0, x];
        addressableMemory.Write(registers.I, memory);

        return ProgramCounterResult.Advance;
    }
}
