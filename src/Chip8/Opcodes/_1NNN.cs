using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _1NNN
{
    public static ProgramCounterResult Execute(IRegisters registers, ushort nnn)
    {
        registers.ProgramCounter.SetValue(nnn);
        return ProgramCounterResult.Stay;
    }
}
