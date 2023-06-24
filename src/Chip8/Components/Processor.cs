using Chip8.Extensions;
using Chip8.Model.Components;
using Chip8.Model.IO;
using Chip8.Model.Sprites;
using Chip8.Opcodes;

namespace Chip8.Components;

public class Processor : IProcessor
{
    private const byte A = 10, B = 11, C = 12, D = 13, E = 14, F = 15;
    ProgramCounterHint lastResult = ProgramCounterHint.Advance;
    Opcode? lastOpcode;
    
    public ProgramCounterHint ExecuteOpcode(IAddressableMemory addressableMemory, IFrameBuffer frameBuffer, IStack stack, IRegisters registers, ITimers timers, IFont font, IKeyboard keyboard)
    {
        lastResult = lastResult switch
        {
            ProgramCounterHint.WaitForKey when keyboard.PressedKey is null => lastResult,
            _ => ExecuteOpcode(lastOpcode = addressableMemory.Read(registers.ProgramCounter, sizeof(ushort)), addressableMemory, frameBuffer, stack, registers, timers, font, keyboard)
        };

        AdvanceProgramCounterIfNeeded(registers.ProgramCounter, lastResult);

        return lastResult;
    }

    public Opcode? LastOpcode => lastOpcode;

    private static void AdvanceProgramCounterIfNeeded(IProgramCounter programCounter, ProgramCounterHint result)
    {
        switch (result)
        {
            case ProgramCounterHint.SkipOneThenAdvance:
                programCounter.AdvanceToNextOpcode();
                programCounter.AdvanceToNextOpcode();
                break;

            case ProgramCounterHint.Advance:
                programCounter.AdvanceToNextOpcode();
                break;
        }
    }

    private static ProgramCounterHint ExecuteOpcode(Opcode op, IAddressableMemory addressableMemory, IFrameBuffer frameBuffer, IStack stack, IRegisters registers, ITimers timers, IFont font, IKeyboard keyboard)
    {
        return op switch
        {
            (0, 0, E, 0) => _00E0.Execute(frameBuffer),
            (0, 0, E, E) => _00EE.Execute(stack, registers),
            (0, _, _, _) => _0NNN.Execute(op.NNN),
            (1, _, _, _) => _1NNN.Execute(registers, op.NNN),
            (2, _, _, _) => _2NNN.Execute(stack, registers, op.NNN),
            (3, _, _, _) => _3XNN.Execute(registers, op.X, op.NN),
            (4, _, _, _) => _4XNN.Execute(registers, op.X, op.NN),
            (5, _, _, 0) => _5XY0.Execute(registers, op.X, op.Y),
            (6, _, _, _) => _6XNN.Execute(registers, op.X, op.NN),
            (7, _, _, _) => _7XNN.Execute(registers, op.X, op.NN),
            (8, _, _, 0) => _8XY0.Execute(registers, op.X, op.Y),
            (8, _, _, 1) => _8XY1.Execute(registers, op.X, op.Y),
            (8, _, _, 2) => _8XY2.Execute(registers, op.X, op.Y),
            (8, _, _, 3) => _8XY3.Execute(registers, op.X, op.Y),
            (8, _, _, 4) => _8XY4.Execute(registers, op.X, op.Y),
            (8, _, _, 5) => _8XY5.Execute(registers, op.X, op.Y),
            (8, _, _, 6) => _8XY6.Execute(registers, op.X),
            (8, _, _, 7) => _8XY7.Execute(registers, op.X, op.Y),
            (8, _, _, E) => _8XYE.Execute(registers, op.X),
            (9, _, _, 0) => _9XY0.Execute(registers, op.X, op.Y),
            (A, _, _, _) => _ANNN.Execute(registers, op.NNN),
            (B, _, _, _) => _BNNN.Execute(registers, op.NNN),
            (C, _, _, _) => _CXNN.Execute(registers, op.X, op.NN),
            (D, _, _, _) => _DXYN.Execute(registers, addressableMemory, frameBuffer, op.X, op.Y, op.N),
            (E, _, 9, E) => _EX9E.Execute(registers, keyboard, op.X),
            (E, _, A, 1) => _EXA1.Execute(registers, keyboard, op.X),
            (F, _, 0, 7) => _FX07.Execute(registers, timers, op.X),
            (F, _, 0, A) => _FX0A.Execute(registers, keyboard, op.X),
            (F, _, 1, 5) => _FX15.Execute(registers, timers, op.X),
            (F, _, 1, 8) => _FX18.Execute(registers, timers, op.X),
            (F, _, 1, E) => _FX1E.Execute(registers, op.X),
            (F, _, 2, 9) => _FX29.Execute(registers, font, op.X),
            (F, _, 3, 3) => _FX33.Execute(registers, addressableMemory, op.X),
            (F, _, 5, 5) => _FX55.Execute(registers, addressableMemory, op.X),
            (F, _, 6, 5) => _FX65.Execute(registers, addressableMemory, op.X),
            _ => throw new NotImplementedException($"Opcode {op} not implemented")
        };
    }
}
