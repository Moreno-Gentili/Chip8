using Chip8.Model.Components;

namespace Chip8.Extensions
{
    public static class StackPointerExtensions
    {
        public static void Increment(this IStackPointer stackPointer)
        {
            stackPointer.Change(1);
        }

        public static void Decrement(this IStackPointer stackPointer)
        {
            stackPointer.Change(-1);
        }

        private static void Change(this IStackPointer stackPointer, int delta)
        {
            stackPointer.SetValue(Convert.ToByte(stackPointer.GetValue() + delta));
        }
    }
}
