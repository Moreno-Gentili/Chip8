using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX65
{
    public static ExecuteResult Execute(IRegisters registers, IAddressableMemory addressableMemory, byte x)
    {
        return ExecuteResult.Proceed;
    }
}
