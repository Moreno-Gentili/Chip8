using Chip8.Model.Components;

namespace Chip8.Extensions
{
    public static class ProgramCounterExtensions
    {
        public static void Increment(this IProgramCounter programCounter, int value = 1)
        {
            programCounter.SetValue(Convert.ToUInt16(programCounter.GetValue() + value));
        }

        public static void SkipNext(this IProgramCounter programCounter)
        {
            programCounter.Increment();
        }
    }
}
