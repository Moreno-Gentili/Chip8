using Chip8.Model.Sprites;
namespace Chip8.Sprites;

// Each font character is four pixels wide by five pixels tall.
// Since the characters are 4 pixels wide but sprites are 8 pixels wide, I'll pad the other 4 pixels with zeros.
public record DefaultFont : IFont
{
    private Dictionary<FontDigit, ISprite> digits = new Dictionary<FontDigit, ISprite>
    {
        { FontDigit.Digit0, new FontSprite(0xF0, 0x90, 0x90, 0x90, 0xF0) },
        { FontDigit.Digit1, new FontSprite(0x20, 0x60, 0x20, 0x20, 0x70) },
        { FontDigit.Digit2, new FontSprite(0xF0, 0x10, 0xF0, 0x80, 0xF0) },
        { FontDigit.Digit3, new FontSprite(0xF0, 0x10, 0xF0, 0x10, 0xF0) },
        { FontDigit.Digit4, new FontSprite(0x90, 0x90, 0xF0, 0x10, 0x10) },
        { FontDigit.Digit5, new FontSprite(0xF0, 0x80, 0xF0, 0x10, 0xF0) },
        { FontDigit.Digit6, new FontSprite(0xF0, 0x80, 0xF0, 0x90, 0xF0) },
        { FontDigit.Digit7, new FontSprite(0xF0, 0x10, 0x20, 0x40, 0x40) },
        { FontDigit.Digit8, new FontSprite(0xF0, 0x90, 0xF0, 0x90, 0xF0) },
        { FontDigit.Digit9, new FontSprite(0xF0, 0x90, 0xF0, 0x10, 0xF0) },
        { FontDigit.DigitA, new FontSprite(0xF0, 0x90, 0xF0, 0x90, 0x90) },
        { FontDigit.DigitB, new FontSprite(0xE0, 0x90, 0xE0, 0x90, 0xE0) },
        { FontDigit.DigitC, new FontSprite(0xF0, 0x80, 0x80, 0x80, 0xF0) },
        { FontDigit.DigitD, new FontSprite(0xE0, 0x90, 0x90, 0x90, 0xE0) },
        { FontDigit.DigitE, new FontSprite(0xF0, 0x80, 0xF0, 0x80, 0xF0) },
        { FontDigit.DigitF, new FontSprite(0xF0, 0x80, 0xF0, 0x80, 0x80) }
    };

    public Dictionary<FontDigit, ISprite> Digits => digits;

    public ushort GetDigitLocation(FontDigit digit)
    {
        ISprite sprite = digits[digit];
        return Convert.ToUInt16(sprite.Memory.Length * (int)digit);
    }
}