namespace Chip8.Model.Components;

public interface IRegisters
{
    // Chip-8 has 16 general purpose 8-bit registers, usually referred to as Vx, where x is a hexadecimal digit(0 through F).
    IRegister<byte> V0 { get; }
    IRegister<byte> V1 { get; }
    IRegister<byte> V2 { get; }
    IRegister<byte> V3 { get; }
    IRegister<byte> V4 { get; }
    IRegister<byte> V5 { get; }
    IRegister<byte> V6 { get; }
    IRegister<byte> V7 { get; }
    IRegister<byte> V8 { get; }
    IRegister<byte> V9 { get; }
    IRegister<byte> VA { get; }
    IRegister<byte> VB { get; }
    IRegister<byte> VC { get; }
    IRegister<byte> VD { get; }
    IRegister<byte> VE { get; }
    IRegister<byte> VF { get; }

    // There is also a 16-bit register called I.
    // This register is generally used to store memory addresses, so only the lowest (rightmost) 12 bits are usually used.
    IRegister<ushort> I { get; }
}
