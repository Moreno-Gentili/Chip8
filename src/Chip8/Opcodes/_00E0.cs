using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _00E0
{
    public static ProgramCounterResult Execute(IFrameBuffer frameBuffer)
    {
        frameBuffer.Clear();
        return ProgramCounterResult.Advance;
    }
}
