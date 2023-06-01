﻿using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Opcodes;

public static class _EX9E
{
    public static ExecuteResult Execute(IRegisters registers, IKeyboard keyboard, byte x)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        byte value = register.GetValue();

        if (keyboard.PressedKey == (Key)value)
        {
            registers.ProgramCounter.SkipNext();
        }

        return ExecuteResult.Proceed;
    }
}
