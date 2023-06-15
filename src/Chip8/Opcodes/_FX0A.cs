using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Opcodes;

public static class _FX0A
{
    public static ProgramCounterResult Execute(IRegisters registers, IKeyboard keyboard, RegisterName x)
    {
        if (!keyboard.PressedKey.HasValue)
        {
            return ProgramCounterResult.WaitingForKey;
        }

        byte value = (byte)keyboard.PressedKey.Value;
        IRegisterV register = registers.V[x];

        register.SetValue(value);

        return ProgramCounterResult.Advance;
    }
}
