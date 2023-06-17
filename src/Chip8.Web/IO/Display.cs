using Aptacode.BlazorCanvas;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Web.IO;

public class Display : IDisplay
{
    private readonly BlazorCanvas canvas;
    private readonly int width;
    private readonly int height;

    public Display(BlazorCanvas canvas, long width, long height)
    {
        this.canvas = canvas;
        this.width = Convert.ToInt32(width);
        this.height = Convert.ToInt32(height);
    }

    public string Color { get; set; } = "green";

    public void Render(IFrameBuffer frameBuffer)
    {
        canvas.ClearRect(0, 0, width, height);
        canvas.FillStyle(Color);
        // await context.SetShadowBlurAsync(10);
        // await context.SetShadowColorAsync(Color);

        int factorX = width / frameBuffer.Width;
        int factorY = height / frameBuffer.Height;

        for (byte x = 0; x < frameBuffer.Width; x++)
        {
            for (byte y = 0; y < frameBuffer.Height; y++)
            {
                bool isPixelOn = frameBuffer[x, y];
                if (isPixelOn)
                {
                    canvas.FillRect(x * factorX, y * factorY, factorX, factorY);
                }
            }
        }
    }
}
