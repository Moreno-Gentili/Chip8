using System.Windows;
using System.Windows.Media;
using Chip8.Wpf.IO;

namespace Chip8;

public class App : Application
{
    [STAThreadAttribute]
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintHelp();
            return;
        }

        string romPath = args[0];
        App app = new App(romPath);
        app.Run();
    }

    public App(string romPath)
    {
        Cassette cassette = new(romPath);
        Keyboard keyboard = new();
        Speaker speaker = new();
        Display display = new(scale: 6, fps: 60, drawScanlines: true, primaryColor: Color.FromRgb(10, 235, 0));
        
        VirtualMachine vm = new();
        vm.Run(cassette, keyboard, display, speaker);

        MainWindow = display;
        MainWindow.Show();
    }

    private static void PrintHelp()
    {
        Console.WriteLine("Please provide rom path");
    }
}