using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _BNNN
{
    public static ProgramCounterResult Execute(IRegisters registers, ushort nnn)
    {
        IRegisterV register = registers.V[RegisterName.V0];
        nnn += register.GetValue();

        registers.ProgramCounter.SetValue(nnn);

        return ProgramCounterResult.Stay;
    }
}
