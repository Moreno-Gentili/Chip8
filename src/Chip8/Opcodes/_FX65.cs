using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX65
{
    public static ExecuteResult Execute(IRegisters registers, IAddressableMemory addressableMemory, byte x)
    {
        registers.V[..(x + 1)] = addressableMemory.Read(registers.I, Convert.ToUInt16(x + 1));
        
        return ExecuteResult.Proceed;
    }
}
