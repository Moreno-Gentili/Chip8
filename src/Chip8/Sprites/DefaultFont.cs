using Chip8.Model.Sprites;
namespace Chip8.Sprites;

// Each font character is four pixels wide by five pixels tall.
// Since the characters are 4 pixels wide but sprites are 8 pixels wide, I'll pad the other 4 pixels with zeros.
public record DefaultFont : IFont
{
    private Sprite[] digits = new Sprite[]
    {
        new Sprite(0xF0, 0x90, 0x90, 0x90, 0xF0),
        new Sprite(0x20, 0x60, 0x20, 0x20, 0x70),
        new Sprite(0xF0, 0x10, 0xF0, 0x80, 0xF0),
        new Sprite(0xF0, 0x10, 0xF0, 0x10, 0xF0),
        new Sprite(0x90, 0x90, 0xF0, 0x10, 0x10),
        new Sprite(0xF0, 0x80, 0xF0, 0x10, 0xF0),
        new Sprite(0xF0, 0x80, 0xF0, 0x90, 0xF0),
        new Sprite(0xF0, 0x10, 0x20, 0x40, 0x40),
        new Sprite(0xF0, 0x90, 0xF0, 0x90, 0xF0),
        new Sprite(0xF0, 0x90, 0xF0, 0x10, 0xF0),
        new Sprite(0xF0, 0x90, 0xF0, 0x90, 0x90),
        new Sprite(0xE0, 0x90, 0xE0, 0x90, 0xE0),
        new Sprite(0xF0, 0x80, 0x80, 0x80, 0xF0),
        new Sprite(0xE0, 0x90, 0x90, 0x90, 0xE0),
        new Sprite(0xF0, 0x80, 0xF0, 0x80, 0xF0),
        new Sprite(0xF0, 0x80, 0xF0, 0x80, 0x80)
    };

    public ISprite GetDigit(FontDigit digit)
    {
        return digits[(int)digit];
    }
}