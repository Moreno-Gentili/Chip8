﻿using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _8XY4
{
    private const byte carry = 1;
    private const byte noCarry = 0;

    public static ProgramCounterHint Execute(IRegisters registers, RegisterId x, RegisterId y)
    {
        IRegisterV registerX = registers.V[x];
        IRegisterV registerY = registers.V[y];
        IRegisterV registerF = registers.V[RegisterId.VF];

        byte valueX = registerX.GetValue();
        byte valueY = registerY.GetValue();

        bool hasCarry = valueX + valueY > byte.MaxValue;
        registerF.SetValue(hasCarry ? carry : noCarry);

        unchecked
        {
            valueX += valueY;
        }

        registerX.SetValue(valueX);

        return ProgramCounterHint.Advance;
    }
}
