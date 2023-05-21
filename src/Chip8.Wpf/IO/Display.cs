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
    private readonly DispatcherTimer timer;
    private const int Columns = 64;
    private const int Rows = 32;
    private readonly int scale;
    private readonly byte[] buffer;
    private readonly Rect rectangle;
    private readonly BitmapPalette bitmapPalette;
    private readonly BitmapSource? scanlines = null;

    public Display(Color primaryColor, int scale = 6, float fps = 60f, bool drawScanlines = true)
    {
        this.scale = scale;
        Width = Columns * scale + 15;
        Height = Rows * scale + 36;
        rectangle = new Rect(0, 0, Columns * scale, Rows * scale);
        ResizeMode = ResizeMode.NoResize;
        BorderThickness = new Thickness(0);
        Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        timer = CreateTimer(fps);
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

    private DispatcherTimer CreateTimer(float fps)
    {
        DispatcherTimer dispatcherTimer = new();
        dispatcherTimer.Tick += new EventHandler(Invalidate);
        dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000 / fps);
        dispatcherTimer.Start();
        return dispatcherTimer;
    }

    private void Invalidate(object? sender, EventArgs e)
    {
        InvalidateVisual();
    }

    protected override void OnRender(DrawingContext dc)
    {
        var random = new Random();
        for (int x = 0; x < Columns; x++)
        {
            for (int y = 0; y < Rows; y++)
            {
                buffer[x + y * Columns] = Convert.ToByte(random.Next(0, 2));
            }
        }

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

    protected override void OnClosing(CancelEventArgs e)
    {
        timer.Stop();
        base.OnClosing(e);
    }

    public void Show(IFrameBuffer frameBuffer)
    {
        
    }
}
