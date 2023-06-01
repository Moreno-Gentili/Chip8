using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY4
{
    private const byte carry = 1;
    private const byte noCarry = 0;

    public static ExecuteResult Execute(IRegisters registers, byte x, byte y)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        IRegisterV registerY = registers.V[(RegisterName)y];
        IRegisterV registerF = registers.V[RegisterName.VF];

        byte valueX = registerX.GetValue();
        byte valueY = registerY.GetValue();

        bool hasCarry = valueX + valueY > byte.MaxValue;
        registerF.SetValue(hasCarry ? carry : noCarry);

        unchecked
        {
            valueX += valueY;
        }

        registerX.SetValue(valueX);

        return ExecuteResult.Proceed;
    }
}
