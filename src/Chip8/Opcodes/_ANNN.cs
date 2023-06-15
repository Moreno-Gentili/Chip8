using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _ANNN
{
    public static ProgramCounterResult Execute(IRegisters registers, ushort nnn)
    {
        registers.I.SetValue(nnn);

        return ProgramCounterResult.Advance;
    }
}
