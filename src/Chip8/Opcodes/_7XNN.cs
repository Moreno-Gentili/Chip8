using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _7XNN
{
    public static ProgramCounterHint Execute(IRegisters registers, RegisterName x, byte nn)
    {
        IRegisterV registerX = registers.V[x];
        byte valueX = registerX.GetValue();
        unchecked
        {
            valueX += nn;
        }

        registerX.SetValue(valueX);

        return ProgramCounterHint.Advance;
    }
}
