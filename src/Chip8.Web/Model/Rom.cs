namespace Chip8.Web.Model;

public record Rom(string Crc32, string Name, string Author, int Year, string Group, string Site, string Url, Dictionary<char, char> KeyMap);
