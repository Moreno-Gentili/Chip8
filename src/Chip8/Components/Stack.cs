namespace Chip8.Components;

public class Stack
{
    private readonly Stack<StackEntry> stack = new();
}

public class StackEntry
{
    private readonly byte[] buffer = new byte[48];
}

