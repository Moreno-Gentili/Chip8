using Chip8.Model.IO;

namespace Chip8.Web.IO;

public class Keyboard : IKeyboard
{
    private Key? pressedKey = null;
    public Key? PressedKey => pressedKey;

    public void HandleKeyDown(string key)
    {
        Key? pressedKey = GetMappedKey(key);

        if (pressedKey is not null)
        {
            this.pressedKey = pressedKey;
        }
    }

    public void HandleKeyUp(string key)
    {
        Key? pressedKey = GetMappedKey(key);
        if (pressedKey is not null && this.pressedKey == pressedKey)
        {
            this.pressedKey = null;
        }
    }

    private Key? GetMappedKey(string key)
    {
        return key switch
        {
            "1" => Key.Key1,
            "2" => Key.Key2,
            "3" => Key.Key3,
            "4" => Key.KeyC,
            "q" => Key.Key4,
            "w" => Key.Key5,
            "e" => Key.Key6,
            "r" => Key.KeyD,
            "a" => Key.Key7,
            "s" => Key.Key8,
            "d" => Key.Key9,
            "f" => Key.KeyE,
            "z" => Key.KeyA,
            "x" => Key.Key0,
            "c" => Key.KeyB,
            "v" => Key.KeyF,
            _ => null
        };
    }
}
