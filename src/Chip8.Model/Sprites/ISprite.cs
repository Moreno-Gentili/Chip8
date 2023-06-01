namespace Chip8.Model.Sprites;

public interface ISprite
{
    byte Width { get; }
    byte Height { get; }
    bool this[byte x, byte y] { get; }
    Memory<byte> Memory { get; }
}