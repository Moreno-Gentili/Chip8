using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Opcodes;

public static class _EXA1
{
    public static ExecuteResult Execute(IRegisters registers, Model.IO.IKeyboard keyboard, byte x)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        byte value = register.GetValue();

        if (keyboard.PressedKey != (Key)value)
        {
            registers.ProgramCounter.ProceedToNextOpcode();
        }

        return ExecuteResult.Proceed;
    }
}
