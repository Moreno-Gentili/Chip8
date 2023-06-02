namespace Chip8.Model.Sprites;

public interface IFont
{
    Dictionary<FontDigit, ISprite> Digits { get; }
    ushort GetDigitPosition(FontDigit digit);
} 

public enum FontDigit
{
    Digit0 = 0,
    Digit1 = 1,
    Digit2 = 2,
    Digit3 = 3,
    Digit4 = 4,
    Digit5 = 5,
    Digit6 = 6,
    Digit7 = 7,
    Digit8 = 8,
    Digit9 = 9,
    DigitA = 10,
    DigitB = 11,
    DigitC = 12,
    DigitD = 13,
    DigitE = 14,
    DigitF = 15
}