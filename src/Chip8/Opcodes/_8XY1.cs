using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY1
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte y)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        IRegisterV registerY = registers.V[(RegisterName)y];

        byte valueX = registerX.GetValue();
        byte valueY = registerY.GetValue();

        valueX |= valueY;
        registerX.SetValue(valueX);

        return ExecuteResult.Proceed;
    }
}
