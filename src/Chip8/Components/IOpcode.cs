namespace Chip8.Model.Components;

public interface IOpcode
{
    OpcodeType Type { get; }
    ushort Value { get; }
}

public enum OpcodeType
{
    _0NNN,
    _00E0,
    _00EE,
    _1NNN,
    _2NNN,
    _3XNN,
    _4XNN,
    _5XY0,
    _6XNN,
    _7XNN,
    _8XY0,
    _8XY1,
    _8XY2,
    _8XY3,
    _8XY4,
    _8XY5,
    _8XY6,
    _8XY7,
    _8XYE,
    _9XY0,
    _ANNN,
    _BNNN,
    _CXNN,
    _DXYN,
    _EX9E,
    _EXA1,
    _FX07,
    _FX0A,
    _FX15,
    _FX18,
    _FX1E,
    _FX29,
    _FX33,
    _FX55,
    _FX65
}