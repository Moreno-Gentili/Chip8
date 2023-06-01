using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _0NNN
{
    public static ExecuteResult Execute(ushort nnn)
    {
        // This instruction is only used on the old computers on which Chip-8 was originally implemented.
        // It is ignored by modern interpreters.
        return ExecuteResult.Proceed;
    }
}
