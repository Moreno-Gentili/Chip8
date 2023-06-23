using Aptacode.BlazorCanvas;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Web.IO;

public partial class Display
{
    private class Crt : IDisplay
    {
        private readonly BlazorCanvas canvas;
        private readonly int width;
        private readonly int height;
        private const int duration = 7;
        private const int displacementHeight = 4;
        private const int displacementWidth = 2;
        private const int displacementMovement = 1;
        private const string displacementColor = "rgba(0, 0, 0, 0.3)";

        public Crt(BlazorCanvas canvas, long width, long height)
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

            int factorX = width / frameBuffer.Width;
            int factorY = height / frameBuffer.Height;

            for (byte y = 0; y < frameBuffer.Height; y++)
            {
                (double position, bool isCurrentLineDisplaced) = GetDisplacementInfo(frameBuffer, y, time);

                for (byte x = 0; x < frameBuffer.Width; x++)
                {
                    bool isPixelOn = frameBuffer[x, y];
                    if (isPixelOn)
                    {
                        if (isCurrentLineDisplaced)
                        {
                            DrawDisplacedPixel(x, y, factorX, factorY, position);
                        }
                        else
                        {
                            DrawRegularPixel(x, y, factorX, factorY);
                        }
                    }
                }
            }

            DrawScanlines(frameBuffer, factorY);
        }

        private static (double position, bool isCurrentLineDisplaced) GetDisplacementInfo(IFrameBuffer frameBuffer, byte y, TimeSpan time)
        {
            double position = time.TotalSeconds % duration / duration * frameBuffer.Height;
            bool isCurrentLineDisplaced = Convert.ToByte(Math.Floor(position)) == y;
            return (position, isCurrentLineDisplaced);
        }

        private void DrawRegularPixel(byte x, byte y, int factorX, int factorY)
        {
            canvas.FillRect(x * factorX, y * factorY, factorX, factorY);
        }

        private void DrawDisplacedPixel(byte x, byte y, int factorX, int factorY, double position)
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

        private void DrawScanlines(IFrameBuffer frameBuffer, int factorY)
        {
            for (byte y = 0; y < frameBuffer.Height; y++)
            {
                canvas.ClearRect(0, (y + 1) * factorY - 1, width, 1);
            }
        }
    }
}