using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY6
{
    public static ProgramCounterHint Execute(IRegisters registers, RegisterName x, RegisterName y)
    {
        IRegisterV registerX = registers.V[x];
        IRegisterV registerY = registers.V[y];
        IRegisterV registerF = registers.V[RegisterName.VF];

        byte valueY = registerY.GetValue();
        byte shiftedOut = Convert.ToByte(valueY & 0x01);
        valueY >>= 1;

        registerX.SetValue(valueY);
        registerF.SetValue(shiftedOut);

        return ProgramCounterHint.Advance;
    }
}
