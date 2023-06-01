using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _7XNN
{
    public static ExecuteResult Execute(IRegisters registers, byte x, byte nn)
    {
        IRegisterV register = registers.V[(RegisterName)x];
        byte vxValue = register.GetValue();
        unchecked
        {
            vxValue += x;
        }

        register.SetValue(vxValue);
        return ExecuteResult.Proceed;
    }
}
