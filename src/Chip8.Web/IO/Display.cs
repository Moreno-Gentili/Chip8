using Aptacode.BlazorCanvas;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Web.IO;

public class Display : IDisplay
{
    private readonly BlazorCanvas canvas;
    private readonly int width;
    private readonly int height;
    private readonly string scanlinesColor = "rgba(0, 0, 0, 0.9)";
    private readonly string displacementColor = "rgba(0, 0, 0, 0.3)";

    public Display(BlazorCanvas canvas, long width, long height)
    {
        this.canvas = canvas;
        this.width = Convert.ToInt32(width);
        this.height = Convert.ToInt32(height);
    }

    public string Color { get; set; } = "lime";

    public void Render(IFrameBuffer frameBuffer, TimeSpan time)
    {
        canvas.ClearRect(0, 0, width, height);
        canvas.FillStyle(Color);
        // await context.SetShadowBlurAsync(10);
        // await context.SetShadowColorAsync(Color);

        bool isCrt = true;
        int factorX = width / frameBuffer.Width;
        int factorY = height / frameBuffer.Height;
        int duration = 7;
        int displacementHeight = 4;
        int displacementWidth = 2;
        int displacementMovement = 1;

        for (byte y = 0; y < frameBuffer.Height; y++)
        {
            double position = ((time.TotalSeconds % duration) / duration) * frameBuffer.Height;
            bool isCurrentLineDisplaced = isCrt && Convert.ToByte(Math.Floor(position)) == y;

            for (byte x = 0; x < frameBuffer.Width; x++)
            {
                bool isPixelOn = frameBuffer[x, y];
                if (isPixelOn)
                {
                    if (isCurrentLineDisplaced)
                    {
                        int displacementOffset = Convert.ToInt32((position % 1) * factorY / displacementMovement) * displacementMovement;
                        if (displacementOffset > 0)
                        {
                            canvas.FillRect(x * factorX, y * factorY, factorX, displacementOffset);
                        }

                        canvas.FillRect(x * factorX + displacementWidth, y * factorY + displacementOffset, factorX, displacementHeight);
                        canvas.FillStyle(displacementColor);
                        canvas.FillRect(x * factorX + displacementWidth, y * factorY + displacementOffset, factorX, displacementHeight);
                        canvas.FillStyle(Color);

                        if (displacementOffset + displacementHeight < factorY)
                        {
                            canvas.FillRect(x * factorX, y * factorY + displacementOffset + displacementHeight, factorX, factorY - displacementOffset - displacementHeight);
                        }
                    }
                    else
                    {
                        canvas.FillRect(x * factorX, y * factorY, factorX, factorY);
                    }
                }
            }
        }

        
        if (isCrt)
        {

            //canvas.FillStyle(scanlinesColor);
            for (byte y = 0; y < frameBuffer.Height; y++)
            {
                //canvas.FillRect(0, (y + 1) * factorY - 1, width, 2);
                canvas.ClearRect(0, (y + 1) * factorY - 1, width, 1);
            }
        }
    }
}
