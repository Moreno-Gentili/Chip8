using Chip8.Model.Components;

namespace Chip8.Components;

public class Register<T> : IRegister<T>
{
    private const int Length = 1;
    private readonly Memory<T> memory;
    public Register(Memory<T> memory)
    {
        if (memory.Length != Length)
        {
            throw new ArgumentException($"Length must be {Length}", nameof(memory));
        }

        this.memory = memory;
    }

    public T GetValue()
    {
        return memory.Span[0];
    }

    public void SetValue(T value)
    {
        memory.Span[0] = value;
    }
}

