using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Chip8.Model.Components;
using Chip8.Model.IO;

namespace Chip8.Wpf.IO;

public class Display : Window, IDisplay
{
    private const int Columns = 64;
    private const int Rows = 32;
    private readonly byte[] buffer;
    private readonly Rect rectangle;
    private readonly BitmapPalette bitmapPalette;
    private readonly BitmapSource? scanlines = null;

    public Display(Color primaryColor, int scale = 6, bool drawScanlines = true)
    {
        Width = Columns * scale + 15;
        Height = Rows * scale + 36;
        rectangle = new Rect(0, 0, Columns * scale, Rows * scale);
        ResizeMode = ResizeMode.NoResize;
        BorderThickness = new Thickness(0);
        Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        bitmapPalette = new BitmapPalette(new List<Color> { Color.FromRgb(0, 0, 0), primaryColor });
        buffer = new byte[Columns * Rows];
        RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
        Title = $"Video output ({Columns}x{Rows})";
        WindowStyle = WindowStyle.SingleBorderWindow;

        if (drawScanlines)
        {
            scanlines = GetScanlines();
        }
    }

    protected override void OnRender(DrawingContext dc)
    {
        BitmapSource pixels = GetImage(buffer, bitmapPalette, Columns, Rows);
        dc.DrawImage(pixels, rectangle);
        if (scanlines is not null)
        {
            dc.DrawImage(scanlines, rectangle);
        }
    }

    private BitmapSource GetImage(byte[] buffer, BitmapPalette palette, int width, int height)
    {
        int bytesPerPixel = 1;
        int stride = 4 * ((width * bytesPerPixel + 3) / 4);
        BitmapSource pixels = BitmapSource.Create(width, height, 300, 300, PixelFormats.Indexed8, palette, buffer, stride);
        return pixels;
    }

    private BitmapSource GetScanlines()
    {
        var width = Columns * 4;
        var height = Rows * 4;
        var buffer = new byte[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y += 2)
            {
                buffer[x + y * width] = 1;
            }
        }

        BitmapPalette palette = new BitmapPalette(new List<Color> { Color.FromArgb(0, 0, 0, 0), Color.FromArgb(80, 0, 0, 0) });
        BitmapSource scanlines = GetImage(buffer, palette, width, height);
        return scanlines;
    }

    public void Show(IFrameBuffer frameBuffer)
    {
        for (byte x = 0; x < frameBuffer.Width; x++)
        {
            for (byte y = 0; y < frameBuffer.Height; y++)
            {
                buffer[x + y * frameBuffer.Width] = Convert.ToByte(frameBuffer[x, y] ? 1 : 0);
            }
        }

        InvalidateVisual();
    }
}
