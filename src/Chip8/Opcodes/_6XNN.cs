using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _6XNN
{
    public static ProgramCounterResult Execute(IRegisters registers, RegisterName x, byte nn)
    {
        IRegisterV register = registers.V[x];
        register.SetValue(nn);

        return ProgramCounterResult.Advance;
    }
}
