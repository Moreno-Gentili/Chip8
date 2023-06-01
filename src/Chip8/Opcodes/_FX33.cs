using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX33
{
    public static ExecuteResult Execute(IRegisters registers, byte x)
    {
        return ExecuteResult.Proceed;
    }
}
