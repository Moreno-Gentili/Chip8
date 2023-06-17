using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX65
{
    public static ProgramCounterHint Execute(IRegisters registers, IAddressableMemory addressableMemory, RegisterId x)
    {
        registers.V[RegisterId.V0, x] = addressableMemory.Read(registers.I, Convert.ToUInt16(x + 1));
        
        return ProgramCounterHint.Advance;
    }
}
