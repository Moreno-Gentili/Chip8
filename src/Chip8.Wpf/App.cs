using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using Chip8.Model;
using Chip8.Wpf.IO;

namespace Chip8;

public class App : Application
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            // PrintHelp();
            // return;
        }

        string romPath = string.Empty;
        App app = new App(romPath);
        app.Run();
    }

    public App(string romPath)
    {
        Cassette cassette = new(romPath);
        Keyboard keyboard = new();
        Speaker speaker = new();
        Clock clock = new(fps: 60);
        Display display = new(scale: 6, drawScanlines: true, primaryColor: Color.FromRgb(10, 235, 0));
        
        IVirtualMachine vm = VirtualMachine.Run(cassette, keyboard, display, speaker, clock);
        // vm.Run(cassette, keyboard, display, speaker);

        MainWindow = display;
        MainWindow.Show();
    }

    private static void PrintHelp()
    {
        Console.WriteLine("Please provide rom path");
    }
}