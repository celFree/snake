# snake
A version of the classic Snake game written in C# and WinForms. 

![Example gameplay](/images/gameplay_static.png)

**This guide is for users who wish to learn more about the technical implementation details about this version of Snake. If you simply want to run the game, see [Getting started](README.md#getting-started).**

## Project structure
The project tree looks like this:

```
|-Snake
  |-src
    |-Logic
    |-Rendering
    |-UI
    |-App.config
    |-Snake.csproj
  |-Snake.sln
```

The source code is split into three parts: logic, rendering and UI handling. Each part is abstracted from each other; for example, logic only focuses on logic, it includes the snake's movement but does not concern itself on how the snake is drawn on the screen, making the logic portable to other platforms.

Logic handles the main game logic, like the snake's movement, where the snake and apple is. It also handles input. 

Rendering handles all graphics and acts as a translation layer between logic and what is seen on screen. Because it must access platform specific drawing libraries (in this case, `System.Drawing.Graphics`) it is not portable.

UI handles most of the graphical initialisation and receives events from the operating system, such as keystrokes and when to terminate. 

I will go into detail how the different parts work and how they work together.

## Logic
The `Logic` directory looks like this. All sources files use the `Snake.Logic` namespace.

```
|-Logic
  |-InputHandler.cs
  |-LogicHandler.cs
```

### `LogicHandler.cs`
This file handles all the discrete game logic, like the snake's location, movement and attributes. 

#### `SnakePoint`
All logic uses the custom type `SnakePoint`, defined like so:

`public record struct SnakePoint(int X, int Y); // line 11`

`SnakePoint` represents a "logic" point where any sort of measurements are ignored, they are just raw coordinates. 
Think of it like a cartesian plane - it does not concern itself with the physical distance between points, or the size of a grid on a plane, it simply represents a point, line, function or graph. It is up to drawer of the plane how big the grids should be. Similarly, logic does not concern itself how the snake is drawn. For all `Snake.Logic` cares, the renderer can completely ignore the specified coordinates. 

### `Direction`
This is a enum defined in the class `LogicHandler`, like so:

```
public enum Direction      // line 27
{
    Up, Down, Left, Right
}
```

`Direction` represents all the cardinal directions. It's primarily used in keeping track of which direction the snake is facing, but it is also used in keeping track of inputs in the input buffer and checking for illegal moves (i.e. when the snake is facing upwards, trying to move directly downwards). 

If one of my goals for this project was not portability, then I could've bound the values of `Direction` to the values of `System.Windows.Forms.Keys`, as it would be slightly faster to process the input from `e.KeyCode` from `Form1.OnKeyDown` and convert it to `Direction`. However, because `LogicHandler` cannot guarantee that the logic will be used on a Windows machine, we convert anyway to maintain portability. On modern machines, the extra delay is negligible anyway.

### `LogicHandler`
This is a pretty important class, which is why I decided to split the LogicHandler.cs section into multiple sub-sections.
