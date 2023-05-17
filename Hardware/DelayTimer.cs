namespace Chip8.Hardware;

// The delay timer is active whenever the DT register is non-zero, when active it will keep decrementing itself at a rate of 60 Hz until it is zero again.
// When zero, it will deactivate itself, not decrementing anymore.
public class DelayTimer
{
    DispatcherTimer
}
