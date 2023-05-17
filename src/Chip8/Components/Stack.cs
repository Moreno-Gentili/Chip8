namespace Chip8.Hardware;

public class Stack
{
    private readonly Stack<StackEntry> stack = new();
}

public class StackEntry
{
    private readonly byte[] buffer = new byte[48];
}

