# Chip-8 emulator
A Chip-8 emulator written in C# for .NET 7, hosted in a Blazor WASM application. 

## Getting started
This solution is composed of 4 projects:
- [src/Chip8.Model] just interfaces modeling the various Chip-8 components;
- [src/Chip8] the actual implementation;
- [src/Chip8.Web] the Blazor WASM host, this is the starting project;
- [test/Chip8.Tests] suite of unit tests.

## Running the application
Clone this repo, than execute this commands:
```
cd src/Chip8.Web
dotnet run
```

## Useful links
- [Design overview](http://www.cs.columbia.edu/~sedwards/classes/2016/4840-spring/designs/Chip8.pdf)
- [Awesome Chip-8](https://github.com/tobiasvl/awesome-chip-8)
- [Wikipedia article on Chip-8](https://en.wikipedia.org/wiki/CHIP-8)
- [OpCodes info #1](https://github.com/mfurga/chip8)
- [OpCodes info #2](https://laurencescotford.com/chip-8-on-the-cosmac-vip-arithmetic-and-logic-instructions/)
- [OpCodes info #3](https://github.com/mattmikolay/chip-8/wiki/CHIP%E2%80%908-Instruction-Set)
- [Virtual machine specification](https://github.com/CaffeineViking/chip-8/blob/master/doc/specification.md)
- [Drawing sprites](http://www.emulator101.com/chip-8-sprites.html)
- [Emulator guide](https://tobiasvl.github.io/blog/write-a-chip-8-emulator/)
- [Test ROM #1](https://github.com/Skosulor/c8int/tree/master/test)
- [Test ROM #2](https://github.com/corax89/chip8-test-rom)
