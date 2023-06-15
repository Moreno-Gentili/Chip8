using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _1NNN
{
    public static ProgramCounterHint Execute(IRegisters registers, ushort nnn)
    {
        registers.ProgramCounter.SetValue(nnn);
        return ProgramCounterHint.Stay;
    }
}
