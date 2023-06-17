using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.Sprites;
using Chip8.Sprites;

namespace Chip8.Opcodes;

public static class _DXYN
{
    public static ProgramCounterHint Execute(IRegisters registers, IAddressableMemory addressableMemory, IFrameBuffer frameBuffer, RegisterId x, RegisterId y, byte n)
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

        IRegisterV registerX = registers.V[x];
        byte valueX = registerX.GetValue();
        IRegisterV registerY = registers.V[y];
        byte valueY = registerY.GetValue();

        DrawResult result = frameBuffer.Draw(sprite, valueX, valueY);
        
        IRegisterV registerF = registers.V[RegisterId.VF];
        registerF.SetValue(Convert.ToByte(result));
        
        return ProgramCounterHint.Advance;
    }
}
