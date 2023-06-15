using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XYE
{
    public static ProgramCounterResult Execute(IRegisters registers, RegisterName x, RegisterName y)
    {
        IRegisterV registerX = registers.V[x];
        IRegisterV registerY = registers.V[y];
        IRegisterV registerF = registers.V[RegisterName.VF];

        byte valueY = registerY.GetValue();
        byte shiftedOut = Convert.ToByte(valueY >> 7);
        valueY <<= 1;

        registerX.SetValue(valueY);
        registerF.SetValue(shiftedOut);

        return ProgramCounterResult.Advance;
    }
}
