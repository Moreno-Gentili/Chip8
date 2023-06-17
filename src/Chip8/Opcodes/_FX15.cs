using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX15
{
    public static ProgramCounterHint Execute(IRegisters registers, ITimers timers, RegisterId x)
    {
        IRegisterV register = registers.V[x];
        byte value = register.GetValue();

        timers.DelayTimer.SetValue(value);

        return ProgramCounterHint.Advance;
    }
}
