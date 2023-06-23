using Chip8.Extensions;
using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _00EE
{
    public static ProgramCounterHint Execute(IStack stack, IRegisters registers)
    {
        registers.StackPointer.Decrement();
        ushort address = stack.GetAddress(registers.StackPointer);
        registers.ProgramCounter.SetValue(address);
        return ProgramCounterHint.Advance;
    }
}
