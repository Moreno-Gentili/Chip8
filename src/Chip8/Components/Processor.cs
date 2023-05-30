using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Components;

public class Processor : IProcessor
{
    bool isAwaitingKeypress = false;
    public IOpcode? ExecuteCycle(IAddressableMemory Memory, IFrameBuffer frameBuffer, IStack stack, IRegisters Registers, ITimers timers, IKeyboard keyboard, ISpeaker speaker)
    {
        if (isAwaitingKeypress && keyboard.PressedKey is null)
        {
            return null;
        }

        return null;
    }
}
