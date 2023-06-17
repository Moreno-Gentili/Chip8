using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Web.IO;

public class Display : IDisplay
{
    private readonly Canvas2DContext context;
    private readonly int width;
    private readonly int height;

    public Display(Canvas2DContext context, long width, long height)
    {
        this.context = context;
        this.width = Convert.ToInt32(width);
        this.height = Convert.ToInt32(height);
    }

    public string Color { get; set; } = "green";

    public async Task Render(IFrameBuffer frameBuffer)
    {
        await context.BeginBatchAsync();
        await context.ClearRectAsync(0, 0, width, height);
        await context.SetFillStyleAsync(Color);
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
                    await context.FillRectAsync(x * factorX, y * factorY, factorX, factorY);
                }
            }
        }

        await context.EndBatchAsync();
    }
}
