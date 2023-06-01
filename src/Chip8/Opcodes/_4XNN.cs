using Chip8.Extensions;
using Chip8.Model.Components;
using System.Xml.Linq;

namespace Chip8.Opcodes;

public static class _4XNN
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte nn)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        if (register.GetValue() != nn)
        {
            registers.ProgramCounter.SkipNext();
        }

        return ExecuteResult.Proceed;
    }
}
