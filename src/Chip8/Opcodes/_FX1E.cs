using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX1E
{
    public static ExecuteResult Execute(IRegisters registers, byte x)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        ushort valueI = registers.I.GetValue();
        byte valueX = registerX.GetValue();

        unchecked
        {
            valueI += valueX;
        }

        registers.I.SetValue(valueI);

        return ExecuteResult.Proceed;
    }
}
