namespace Chip8.Model.Components;

public interface IRegisters
{
    // Chip-8 has 16 general purpose 8-bit registers, usually referred to as Vx, where x is a hexadecimal digit(0 through F).
    IRegister<byte> GetVx(RegisterVx name);

    // There is also a 16-bit register called I.
    // This register is generally used to store memory addresses, so only the lowest (rightmost) 12 bits are usually used.
    IRegister<ushort> GetI();
}

public enum RegisterVx
{
    V0 = 0,
    V1 = 1,
    V2 = 2,
    V3 = 3,
    V4 = 4,
    V5 = 5,
    V6 = 6,
    V7 = 7,
    V8 = 8,
    V9 = 9,
    VA = 10,
    VB = 11,
    VC = 12,
    VD = 13,
    VE = 14,
    VF = 15
}