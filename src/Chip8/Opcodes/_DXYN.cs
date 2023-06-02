using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.Sprites;
using Chip8.Sprites;

namespace Chip8.Opcodes;

public static class _DXYN
{
    public static ExecuteResult Execute(IRegisters registers, IAddressableMemory addressableMemory, IFrameBuffer frameBuffer, byte x, byte y, byte n)
    {

        ISprite sprite;
        IRegister<ushort> register = registers.I;
        if (addressableMemory.FontRange.Contains(register.GetValue()))
        {
            byte memoryLength = FontSprite.GetMemorySizeFromHeight(n);
            Memory<byte> memory = addressableMemory.Read(registers.I, memoryLength);
            sprite = new FontSprite(memory);
        }
        else
        {
            Memory<byte> memory = addressableMemory.Read(registers.I, n);
            sprite = new Sprite(memory);
        }

        IRegisterV registerF = registers.V[RegisterName.VF];
        DrawResult result = frameBuffer.Draw(sprite, x, y);
        registerF.SetValue(Convert.ToByte(result));
        
        return ExecuteResult.Proceed;
    }
}
