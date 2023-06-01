using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _CXNN
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte nn)
    {
        byte randomValue = Convert.ToByte(Random.Shared.Next(byte.MinValue, byte.MaxValue));
        IRegisterV registerX = registers.V[(RegisterName)x];
        byte valueX = Convert.ToByte(randomValue & nn);
        registerX.SetValue(valueX);

        return ExecuteResult.Proceed;
    }
}
