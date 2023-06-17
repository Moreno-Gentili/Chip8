using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _3XNN
{
    public static ProgramCounterHint Execute(IRegisters registers, RegisterId x, byte nn)
    {
        IRegisterV register = registers.V[x];

        return register.GetValue() == nn ?
                ProgramCounterHint.SkipOneThenAdvance :
                ProgramCounterHint.Advance;
    }
}
