using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX18
{
    public static ExecuteResult Execute(IRegisters registers, ITimers timers, byte x)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        byte value = register.GetValue();
        timers.SoundTimer.SetValue(value);

        return ExecuteResult.Proceed;
    }
}
