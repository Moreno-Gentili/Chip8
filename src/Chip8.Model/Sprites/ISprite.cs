namespace Chip8.Model.Sprites;

public interface ISprite
{
    int Width { get; }
    int Height { get; }
    bool this[int x, int y] { get; }
}