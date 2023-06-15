using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Chip8.Model;
using Chip8.Model.IO;
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

        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\test1.ch8";
        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\ibm.ch8";
        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\maze-alt.ch8";
        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\particle.ch8";
        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\stars.ch8";
        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\pong.ch8";
        string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\breakout.ch8";
        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\space.ch8";
        // string romPath = "C:\\Progetti\\Chip8\\src\\Chip8.Wpf\\Roms\\tetris.ch8";
        App app = new App(romPath);
        app.Run();
    }

    public App(string romPath)
    {
        Cassette cassette = new(romPath);
        Speaker speaker = new();
        Display display = new(scale: 6, drawScanlines: true, primaryColor: Color.FromRgb(10, 235, 0));
        IKeyboard keyboard = display;
        
        IVirtualMachine vm = VirtualMachine.Create(cassette, keyboard, display, speaker);
        Stopwatch sw = Stopwatch.StartNew();
        new DispatcherTimer(TimeSpan.FromSeconds(1) / 60, DispatcherPriority.Normal, (sender, args) => vm.Update(sw.Elapsed), Dispatcher.CurrentDispatcher);
        // vm.Run(cassette, keyboard, display, speaker);

        MainWindow = display;
        MainWindow.Show();
    }
}