using Chip8.Model.Components;

namespace Chip8.Model.IO;

// The original Chip-8 has a 64x32-pixel monochrome display, where (0, 0) is located on the top-left part
// of the display and (63, 31) is located at the bottom-right. 
public interface IDisplay
{
    void Render(IFrameBuffer frameBuffer, TimeSpan time);
}