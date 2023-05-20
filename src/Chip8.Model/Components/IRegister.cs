namespace Chip8.Model.Components;

public interface IRegister<T>
{
    T GetValue();
    void SetValue(T value);
}