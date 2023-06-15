using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _2NNN
{
    public static ProgramCounterResult Execute(IStack stack, IRegisters registers, ushort nnn)
    {
        registers.StackPointer.Increment();
        stack.SetAddress(registers.StackPointer, registers.ProgramCounter.GetValue());
        registers.ProgramCounter.SetValue(nnn);
        return ProgramCounterResult.Stay;
    }
}
