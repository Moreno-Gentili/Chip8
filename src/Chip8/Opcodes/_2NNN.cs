using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _2NNN
{
    public static ProgramCounterHint Execute(IStack stack, IRegisters registers, ushort nnn)
    {
        stack.SetAddress(registers.StackPointer, registers.ProgramCounter.GetValue());
        registers.StackPointer.Increment();
        registers.ProgramCounter.SetValue(nnn);
        return ProgramCounterHint.Stay;
    }
}
