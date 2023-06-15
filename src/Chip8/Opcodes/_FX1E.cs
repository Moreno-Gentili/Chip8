using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX1E
{
    public static ProgramCounterResult Execute(IRegisters registers, RegisterName x)
    {
        IRegisterV registerX = registers.V[x];
        ushort valueI = registers.I.GetValue();
        byte valueX = registerX.GetValue();

        unchecked
        {
            valueI += valueX;
        }

        registers.I.SetValue(valueI);

        return ProgramCounterResult.Advance;
    }
}
