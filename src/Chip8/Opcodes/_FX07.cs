using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Opcodes;

public static class _FX07
{
    public static ExecuteResult Execute(IRegisters registers, ITimers timers, byte x)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        byte value = timers.DelayTimer.GetValue();
        register.SetValue(value);

        return ExecuteResult.Proceed;
    }
}
