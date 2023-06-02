using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX33
{
    public static ExecuteResult Execute(IRegisters registers, IAddressableMemory memory, byte x)
    {
        IRegisterV registerX = registers.V[(RegisterName)x];
        byte valueX = registerX.GetValue();

        byte hundreds = Convert.ToByte(valueX / 100);
        byte tens = Convert.ToByte((valueX / 10) % 10);
        byte ones = Convert.ToByte(valueX % 10);

        memory.Write(registers.I, new[] { hundreds, tens, ones });

        return ExecuteResult.Proceed;
    }
}
