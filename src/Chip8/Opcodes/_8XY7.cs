using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY7
{
    private const byte borrow = 1;
    private const byte noBorrow = 0;

    public static ExecuteResult Execute(IRegisters registers, byte x, byte y)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        IRegisterV registerY = registers.V[(RegisterName)y];
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

        return ExecuteResult.Proceed;
    }
}
