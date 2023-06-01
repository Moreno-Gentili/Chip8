using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY6
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte y)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        IRegisterV registerY = registers.V[(RegisterName)y];
        IRegisterV registerF = registers.V[RegisterName.VF];

        byte valueY = registerY.GetValue();
        byte shiftedOut = Convert.ToByte(valueY & 0x01);
        valueY >>= 1;
        registerX.SetValue(valueY);
        registerF.SetValue(shiftedOut);

        return ExecuteResult.Proceed;
    }
}
