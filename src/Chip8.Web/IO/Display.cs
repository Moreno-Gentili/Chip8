using Aptacode.BlazorCanvas;
using Chip8.Model.Components;
using Chip8.Model.IO;
using Chip8.Web.Model;

namespace Chip8.Web.IO;

public partial class Display : IDisplay
{
    private readonly BlazorCanvas canvas;
    private readonly int width;
    private readonly int height;
    private IDisplay? display;

    public Display(BlazorCanvas canvas, int width, int height)
    {
        this.canvas = canvas;
        this.width = width;
        this.height = height;
    }

    public void Connect(DisplayType type)
    {
        display = type switch
        {
            DisplayType.Lcd => new Lcd(canvas, width, height),
            DisplayType.Crt => new Crt(canvas, width, height),
            _ => throw new InvalidOperationException("Display type not supported")
        };
    }

    public void Render(IFrameBuffer frameBuffer, TimeSpan time)
    {
        if (display is null)
        {
            throw new InvalidOperationException("Display not connected");
        }

        display.Render(frameBuffer, time);
    }
}
