using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Opcodes;

public static class _EX9E
{
    public static ProgramCounterHint Execute(IRegisters registers, IKeyboard keyboard, RegisterName x)
    {
        IRegisterV register = registers.V[x];
        byte value = register.GetValue();

        return keyboard.PressedKey == (Key)value ?
            ProgramCounterHint.SkipOneThenAdvance :
            ProgramCounterHint.Advance;
    }
}
