using Chip8.Model.Components;

namespace Chip8.Extensions
{
    public static class ProgramCounterExtensions
    {
        public static void Increment(this IProgramCounter programCounter, int value)
        {
            programCounter.SetValue(Convert.ToUInt16(programCounter.GetValue() + value));
        }

        public static void ProceedToNextOpcode(this IProgramCounter programCounter)
        {
            programCounter.Increment(sizeof(ushort));
        }
    }
}
