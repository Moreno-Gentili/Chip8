namespace Chip8.Extensions
{
    public static class MemoryExtensions
    {
        public static Memory<byte> Chunk(this ref Memory<byte> memory, int length)
        {
            Memory<byte> slice = memory.Slice(0, length);
            memory = memory.Slice(length);
            return slice;
        }
    }
}
