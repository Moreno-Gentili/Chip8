using Chip8.Model.Components;
using Chip8.Model.Sprites;

namespace Chip8.Opcodes;

public static class _FX29
{
    public static ExecuteResult Execute(IRegisters registers, IFont font, byte x)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        byte valueX = registerX.GetValue();
        ushort position = font.GetDigitPosition((FontDigit)valueX);
        registers.I.SetValue(position);

        return ExecuteResult.Proceed;
    }
}
