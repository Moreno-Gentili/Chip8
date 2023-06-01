using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX29
{
    public static ExecuteResult Execute(IRegisters registers, Model.Sprites.IFont font, byte x)
    {
        return ExecuteResult.Proceed;
    }
}
