using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX15
{
    public static ExecuteResult Execute(IRegisters registers, ITimers timers, byte x)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        byte value = register.GetValue();
        timers.DelayTimer.SetValue(value);

        return ExecuteResult.Proceed;
    }
}
