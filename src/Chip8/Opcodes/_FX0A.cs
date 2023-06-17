using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Opcodes;

public static class _FX0A
{
    public static ProgramCounterHint Execute(IRegisters registers, IKeyboard keyboard, RegisterId x)
    {
        if (!keyboard.PressedKey.HasValue)
        {
            return ProgramCounterHint.WaitForKey;
        }

        byte value = (byte)keyboard.PressedKey.Value;
        IRegisterV register = registers.V[x];

        register.SetValue(value);

        return ProgramCounterHint.Advance;
    }
}
