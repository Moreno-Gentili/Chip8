namespace Chip8.Model.IO;

// The official Chip-8 keyboard has 16 buttons, and logically, thus has hexadecimal labels.
// This is the only input type the user has access to.
public interface IKeyboard
{
    Key PressedKeys { get; }
}

[Flags]
public enum Key
{
    Key0 = 1,
    Key1 = 2,
    Key2 = 4,
    Key3 = 8,
    Key4 = 16,
    Key5 = 32,
    Key6 = 64,
    Key7 = 128,
    Key8 = 256,
    Key9 = 512,
    KeyA = 1024,
    KeyB = 2048,
    KeyC = 4096,
    KeyD = 8192,
    KeyE = 16384,
    KeyF = 32768
}