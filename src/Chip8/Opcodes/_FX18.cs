using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _FX18
{
    public static ProgramCounterHint Execute(IRegisters registers, ITimers timers, RegisterId x)
    {
        IRegisterV register = registers.V[x];
        byte value = register.GetValue();

        timers.SoundTimer.SetValue(value);

        return ProgramCounterHint.Advance;
    }
}
