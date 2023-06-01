using Chip8.Model.Components;

namespace Chip8.Opcodes;

public static class _00E0
{
    public static ExecuteResult Execute(IFrameBuffer frameBuffer)
    {
        frameBuffer.Clear();
        return ExecuteResult.Proceed;
    }
}
