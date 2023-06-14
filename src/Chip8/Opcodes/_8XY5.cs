using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY5
{
    private const byte borrow = 0;
    private const byte noBorrow = 1;

    public static ExecuteResult Execute(IRegisters registers, byte x, byte y)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        IRegisterV registerY = registers.V[(RegisterName)y];
        IRegisterV registerF = registers.V[RegisterName.VF];

        byte valueX = registerX.GetValue();
        byte valueY = registerY.GetValue();

        bool hasBorrow = valueY > valueX;
        registerF.SetValue(hasBorrow ? borrow : noBorrow);

        unchecked
        {
            valueX -= valueY;
        }

        registerX.SetValue(valueX);

        return ExecuteResult.Proceed;
    }
}
