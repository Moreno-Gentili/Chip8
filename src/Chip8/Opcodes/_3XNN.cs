using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _3XNN
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte nn)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        if (register.GetValue() == nn)
        {
            registers.ProgramCounter.ProceedToNextOpcode();
        }

        return ExecuteResult.Proceed;
    }
}
