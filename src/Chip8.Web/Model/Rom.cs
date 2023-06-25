using Chip8.Model.IO;

namespace Chip8.Web.Model;

public record Rom(string Crc32, string Name, string? Author, int? Year, string Group, string Url, int? Ips, Dictionary<char, char>? KeyMap)
{
    public Dictionary<char, Key>? GetKeyMap()
    {
        if (KeyMap is null)
        {
            return null;
        }

        return KeyMap.ToDictionary(entry => entry.Value, entry => MapToKey(entry.Key));
    }

    private static Key MapToKey(char key)
    {
        return Enum.Parse<Key>($"Key{key}", ignoreCase: true);
    }
}
