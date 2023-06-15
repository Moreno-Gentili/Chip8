using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _CXNN
{
    public static ProgramCounterResult Execute(IRegisters registers, RegisterName x, byte nn)
    {
        byte randomValue = Convert.ToByte(Random.Shared.Next(byte.MinValue, byte.MaxValue));
        IRegisterV registerX = registers.V[x];
        byte valueX = Convert.ToByte(randomValue & nn);

        registerX.SetValue(valueX);

        return ProgramCounterResult.Advance;
    }
}
