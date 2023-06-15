using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY3
{
    public static ProgramCounterHint Execute(IRegisters registers, RegisterName x, RegisterName y)
    {
        IRegisterV registerX = registers.V[x];
        IRegisterV registerY = registers.V[y];

        byte valueX = registerX.GetValue();
        byte valueY = registerY.GetValue();

        valueX ^= valueY;
        registerX.SetValue(valueX);

        return ProgramCounterHint.Advance;
    }
}
