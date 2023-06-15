using Chip8.Model.Components;
using Chip8.Model.Sprites;

namespace Chip8.Opcodes;

public static class _FX29
{
    public static ProgramCounterHint Execute(IRegisters registers, IFont font, RegisterName x)
    {
        IRegisterV registerX = registers.V[x];
        FontDigit valueX = (FontDigit)registerX.GetValue();
        ushort position = font.GetDigitPosition(valueX);

        registers.I.SetValue(position);

        return ProgramCounterHint.Advance;
    }
}
