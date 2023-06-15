﻿using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _00EE
{
    public static ProgramCounterHint Execute(IStack stack, IRegisters registers)
    {
        ushort address = stack.GetAddress(registers.StackPointer);
        registers.ProgramCounter.SetValue(address);
        registers.StackPointer.Decrement();
        return ProgramCounterHint.Advance;
    }
}
