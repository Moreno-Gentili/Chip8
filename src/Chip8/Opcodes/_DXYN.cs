using Chip8.Model.Components;
using Chip8.Model.Sprites;
using Chip8.Sprites;

namespace Chip8.Opcodes;

public static class _DXYN
{
    public static ExecuteResult Execute(IRegisters registers, IAddressableMemory addressableMemory, IFrameBuffer frameBuffer, byte x, byte y, byte n)
    {
        IRegisterV registerF = registers.V[RegisterName.VF];
        Memory<byte> rows = addressableMemory.Read(registers.I, n);
        ISprite sprite = new Sprite(rows);
        DrawResult result = frameBuffer.Draw(sprite, x, y);
        registerF.SetValue(Convert.ToByte(result));
        
        return ExecuteResult.Proceed;
    }
}
