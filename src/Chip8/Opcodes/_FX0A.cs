using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Opcodes;

public static class _FX0A
{
    public static ExecuteResult Execute(IRegisters registers, IKeyboard keyboard, byte x)
    {
        if (!keyboard.PressedKey.HasValue)
        {
            return ExecuteResult.WaitingForKey;
        }

        byte value = (byte)keyboard.PressedKey.Value;
        IRegisterV register = registers.V[(RegisterName)x];
        register.SetValue(value);

        return ExecuteResult.Proceed;
    }
}
