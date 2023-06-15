using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _ANNN
{
    public static ProgramCounterHint Execute(IRegisters registers, ushort nnn)
    {
        registers.I.SetValue(nnn);

        return ProgramCounterHint.Advance;
    }
}
