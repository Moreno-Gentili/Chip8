using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX07
{
    public static ProgramCounterResult Execute(IRegisters registers, ITimers timers, RegisterName x)
    {
        IRegisterV register = registers.V[x];
        byte value = timers.DelayTimer.GetValue();

        register.SetValue(value);

        return ProgramCounterResult.Advance;
    }
}
