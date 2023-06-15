using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _4XNN
{
    public static ProgramCounterResult Execute(IRegisters registers, RegisterName x, byte nn)
    {
        IRegisterV register = registers.V[x];

        return register.GetValue() != nn ?
            ProgramCounterResult.SkipOneThenAdvance :
            ProgramCounterResult.Advance;
    }
}
