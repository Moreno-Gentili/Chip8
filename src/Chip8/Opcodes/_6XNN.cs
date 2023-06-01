using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _6XNN
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte nn)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        register.SetValue(nn);
        return ExecuteResult.Proceed;
    }
}
