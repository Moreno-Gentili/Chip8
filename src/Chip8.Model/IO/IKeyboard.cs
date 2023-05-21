namespace Chip8.Model.IO;

// The official Chip-8 keyboard has 16 buttons, and logically, thus has hexadecimal labels.
// This is the only input type the user has access to.
public interface IKeyboard
{
    Key? PressedKey { get; }
}

public enum Key
{
    Key0 = 0,
    Key1 = 1,
    Key2 = 2,
    Key3 = 3,
    Key4 = 4,
    Key5 = 5,
    Key6 = 6,
    Key7 = 7,
    Key8 = 8,
    Key9 = 9,
    KeyA = 10,
    KeyB = 11,
    KeyC = 12,
    KeyD = 13,
    KeyE = 14,
    KeyF = 15
}