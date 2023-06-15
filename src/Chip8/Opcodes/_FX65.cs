using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX65
{
    public static ProgramCounterResult Execute(IRegisters registers, IAddressableMemory addressableMemory, RegisterName x)
    {
        registers.V[RegisterName.V0, x] = addressableMemory.Read(registers.I, Convert.ToUInt16(x + 1));
        
        return ProgramCounterResult.Advance;
    }
}
