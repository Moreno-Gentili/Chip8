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

    public ISprite Digit0 => digits[0];
    public ISprite Digit1 => digits[1];
    public ISprite Digit2 => digits[2];
    public ISprite Digit3 => digits[3];
    public ISprite Digit4 => digits[4];
    public ISprite Digit5 => digits[5];
    public ISprite Digit6 => digits[6];
    public ISprite Digit7 => digits[7];
    public ISprite Digit8 => digits[8];
    public ISprite Digit9 => digits[9];
    public ISprite DigitA => digits[10];
    public ISprite DigitB => digits[11];
    public ISprite DigitC => digits[12];
    public ISprite DigitD => digits[13];
    public ISprite DigitE => digits[14];
    public ISprite DigitF => digits[15];
}