﻿using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XYE
{
    public static ProgramCounterHint Execute(IRegisters registers, RegisterId x)
    {
        IRegisterV registerX = registers.V[x];
        IRegisterV registerF = registers.V[RegisterId.VF];

        byte valueX = registerX.GetValue();
        byte shiftedOut = Convert.ToByte(valueX >> 7);
        valueX <<= 1;

        registerX.SetValue(valueX);
        registerF.SetValue(shiftedOut);

        return ProgramCounterHint.Advance;
    }
}
