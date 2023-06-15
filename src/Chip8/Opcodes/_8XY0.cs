using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY0
{
    public static ProgramCounterResult Execute(IRegisters registers, RegisterName x, RegisterName y)
    {
        IRegisterV registerX = registers.V[x];
        IRegisterV registerY = registers.V[y];

        registerX.SetValue(registerY.GetValue());
        return ProgramCounterResult.Advance;
    }
}
