using Chip8.Model.IO;

namespace Chip8.Web.IO;

public class Keyboard : IKeyboard
{
    private Key? pressedKey = null;
    public Key? PressedKey => pressedKey;

    private Dictionary<char, Key> keyMap = new()
    {
        { '1', Key.Key1 },
        { '2', Key.Key2 },
        { '3', Key.Key3 },
        { '4', Key.KeyC },
        { 'Q', Key.Key4 },
        { 'W', Key.Key5 },
        { 'E', Key.Key6 },
        { 'R', Key.KeyD },
        { 'A', Key.Key7 },
        { 'S', Key.Key8 },
        { 'D', Key.Key9 },
        { 'F', Key.KeyE },
        { 'Z', Key.KeyA },
        { 'X', Key.Key0 },
        { 'C', Key.KeyB },
        { 'V', Key.KeyF }
    };

    public IReadOnlyCollection<KeyValuePair<char, Key>> Keys => keyMap;

    public void HandleKeyDown(char key, Dictionary<char, Key>? additionalKeyMap = null)
    {
        Key? mappedKey = GetMappedKey(key, additionalKeyMap);

        if (mappedKey is not null)
        {
            HandleKeyDown(mappedKey.Value);
        }
    }

    public void HandleKeyDown(Key key)
    {
        pressedKey = key;
    }

    public void HandleKeyUp(char key, Dictionary<char, Key>? additionalKeyMap = null)
    {
        Key? mappedKey = GetMappedKey(key, additionalKeyMap);
        
        if (mappedKey is not null)
        {
            HandleKeyUp(mappedKey.Value);
        }
    }

    public void HandleKeyUp(Key key)
    {
        if (pressedKey == key)
        {
            pressedKey = null;
        }
    }

    private Key? GetMappedKey(char key, Dictionary<char, Key>? additionalKeyMap)
    {
        if (keyMap.ContainsKey(key))
        {
            return keyMap[key];
        }
        else if (additionalKeyMap?.ContainsKey(key) == true)
        {
            return additionalKeyMap[key];
        }
        else
        {
            return null;
        }
    }
}
