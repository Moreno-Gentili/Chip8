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
    Key0,
    Key1,
    Key2,
    Key3,
    Key4,
    Key5,
    Key6,
    Key7,
    Key8,
    Key9,
    KeyA,
    KeyB,
    KeyC,
    KeyD,
    KeyE,
    KeyF
}