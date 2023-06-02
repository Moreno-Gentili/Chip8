namespace Chip8.Extensions
{
    public static class RangeExtensions
    {
        public static bool Contains(this Range range, ushort location)
        {
            return range.Start.Value <= location && range.End.Value > location;
        }
    }
}
