using Chip8.Model.Components;

namespace Chip8.Components;

public class Memory : IMemory
{
    private ushort currentPosition = 0;
    private readonly byte[] buffer = new byte[4096];

    public Span<byte> ReadFromCurrentPostion(ushort length)
    {
        return ((Span<byte>)buffer)[currentPosition..length];
    }

    public void SetCurrentPosition(ushort address)
    {
        currentPosition = address;
    }

    public void WriteToCurrentPostion(byte[] data)
    {
        Array.Copy(data, buffer, data.Length);
    }
}