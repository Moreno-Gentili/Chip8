using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY7
{
    private const byte borrow = 0;
    private const byte noBorrow = 1;

    public static ProgramCounterHint Execute(IRegisters registers, RegisterName x, RegisterName y)
    {
        IRegisterV registerX = registers.V[x];
        IRegisterV registerY = registers.V[y];
        IRegisterV registerF = registers.V[RegisterName.VF];

        byte valueX = registerX.GetValue();
        byte valueY = registerY.GetValue();

        bool hasBorrow = valueX > valueY;
        registerF.SetValue(hasBorrow ? borrow : noBorrow);

        unchecked
        {
            valueY -= valueX;
        }

        registerX.SetValue(valueY);

        return ProgramCounterHint.Advance;
    }
}
