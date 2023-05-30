using Chip8.Model.Sprites;

namespace Chip8.Model.Components;

// All sprites are rendered using an exclusive-or (XOR) mode; when a request to draw a sprite is processed, the given sprite's data is XOR'd with the current graphics data of the screen.
public interface IFrameBuffer
{
    void Draw(ISprite sprite, byte x, byte y);
    void Clear();
    bool this[byte x, byte y] { get; }
    int Width { get; }
    int Height { get; }
}
