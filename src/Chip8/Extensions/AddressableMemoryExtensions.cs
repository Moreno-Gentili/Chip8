using Chip8.Model.Components;
using Chip8.Model.Sprites;

namespace Chip8.Extensions
{
    public static class AddresableMemoryExtensions
    {
        public static void WriteSprite(this IAddressableMemory addressableMemory, ISprite sprite, IProgramCounter programCounter)
        {
            addressableMemory.Write(programCounter, sprite.Memory);
            programCounter.Increment(sprite.Memory.Length);
        }
    }
}
