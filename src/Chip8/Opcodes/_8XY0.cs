using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY0
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte y)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        IRegisterV registerY = registers.V[(RegisterName)y];

        registerX.SetValue(registerY.GetValue());
        return ExecuteResult.Proceed;
    }
}
