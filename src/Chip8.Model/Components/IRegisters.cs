namespace Chip8.Model.Components;

public interface IRegisters
{
    // Chip-8 has 16 general purpose 8-bit registers, usually referred to as Vx, where x is a hexadecimal digit(0 through F).
    IRegisterVCollection V { get; }

    // There is also a 16-bit register called I.
    // This register is generally used to store memory addresses, so only the lowest (rightmost) 12 bits are usually used.
    IRegisterI I { get; }

    // The program counter (PC) should be 16-bit, and is used to store the currently executing address.
    IProgramCounter ProgramCounter { get; }
    
    // The program counter(PC) should be 16-bit, and is used to store the currently executing address.
    IStackPointer StackPointer { get; }
}

public interface IRegisterV : IRegister<byte>
{
}

public interface IRegisterVCollection
{
    IRegisterV this[RegisterId registerId] { get; }
    Memory<byte> this[RegisterId fromRegisterId, RegisterId toRegisterId] { get; set; }
}

public interface IRegisterI : IRegister<ushort>
{
}

public interface IProgramCounter : IRegister<ushort>
{
}

public interface IStackPointer : IRegister<byte>
{
}

public enum RegisterId : byte
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