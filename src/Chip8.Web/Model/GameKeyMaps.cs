using Chip8.Model.IO;

namespace Chip8.Web.Model;

public static class GameKeyMaps
{
    const char Left = '←';
    const char Right = '→';
    const char Up = '↑';
    const char Down = '↓';
    const char Action = '␣';

    public static Dictionary<char, Key>? FindGameMapByHash(string? hash)
    {
        return hash switch
        {
            "DCD19CB9" => GetSpaceInvadersMapping(),
            _ => null
        };
    }

    private static Dictionary<char, Key> GetSpaceInvadersMapping() =>
        new() { { Left, Key.Key4 }, { Right, Key.Key6 } };
}
