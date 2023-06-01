using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _5XY0
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte y)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        IRegisterV registerY = registers.V[(RegisterName)y];

        byte valueX = registerX.GetValue();
        byte valueY = registerY.GetValue();

        if (valueX == valueY)
        {
            registers.ProgramCounter.SkipNext();
        }

        return ExecuteResult.Proceed;
    }
}
