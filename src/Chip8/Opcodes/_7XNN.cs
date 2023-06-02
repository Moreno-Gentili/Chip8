using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _7XNN
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte nn)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        byte valueX = registerX.GetValue();
        unchecked
        {
            valueX += nn;
        }

        registerX.SetValue(valueX);
        return ExecuteResult.Proceed;
    }
}
