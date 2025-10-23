# snake
A version of the classic Snake game written in C# and WinForms. 

![Example gameplay](/images/gameplay_static.png)

**This guide is for users who wish to learn more about the technical implementation details and my programming philosophy behind this version of Snake. If you simply want to run the game, see [Getting started](README.md#getting-started).**

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

#### `Direction`
This is a enum defined in the class `LogicHandler`, like so:

```
public enum Direction      // line 27
{
    Up, Down, Left, Right
}
```

`Direction` represents all the cardinal directions. It's primarily used in keeping track of which direction the snake is facing, but it is also used in keeping track of inputs in the input buffer and checking for illegal moves (i.e. when the snake is facing upwards, trying to move directly downwards). 

If one of my goals for this project was not portability, then I could've bound the values of `Direction` to the values of `System.Windows.Forms.Keys`, as it would be slightly faster to process the input from `e.KeyCode` from `Form1.OnKeyDown` and convert it to `Direction`. However, because `LogicHandler` cannot guarantee that the logic will be used on a Windows machine, we convert anyway to maintain portability. On modern machines, the extra delay is negligible anyway.

#### `LogicHandler`
`LogicHandler` is defined like so:
```
internal class LogicHandler
{
    // ...
}
```

##### Members and events
```
List<SnakePoint> snakeBody = [new SnakePoint(5, 8), new SnakePoint(4, 8)];

SnakePoint apple = new SnakePoint(11, 8);

int score = 0;

Dictionary<Direction, SnakePoint> directions = new Dictionary<Direction, SnakePoint>()
{
    { Direction.Up, new SnakePoint(0, -1) },
    { Direction.Down, new SnakePoint(0, 1) },
    { Direction.Left, new SnakePoint(-1, 0) },
    { Direction.Right, new SnakePoint(1, 0) }
};

Direction current = Direction.Right;

InputHandler input;

Random r = new Random();

public event EventHandler? RequestRedraw;
```

+ `snakeBody` is a list of `SnakePoint`s that make up the snake's body. This should be self explanatory, but oh well. Each point is 1pt apart, none *should* overlap or cross over.
+ `apple` is represents the apple's location. There is no reason to call `apple` an apple, other than the fact that I've played way too much Google Snake and just adopted the fruit they used. Either way, it's the object the player aims to obtain to increase their score.
+ `score` represents the player's score.
+ `directions` are the increments needed to move a certain direction. This is to remove the need for a "magic constant".
+ `current` represents which `Direction` the snake's head is facing.
+ `input` is an instance of `InputHandler`. The specifics of `InputHandler` will be explained later, just know that `LogicHandler` grabs inputs already translated to `Direction`s and moves accordingly.
+ `r` is an instance of `Random`. It is used to randomly generate the coordinates of the object.
+ `RequestRedraw` is an nullable event that is raised when the logic handler wants a renderer to redraw. Why not use `Control.Invalidate()` then? The problem is that then makes the logic dependent on `System.Windows.Forms` which we can't guarantee that the renderer or the UI uses. Additionally, it forces the relevant renderers to redraw, when we shouldn't concern ourselves on renderer details. We shouldn't care if the renderer redraws or not; that is up to the renderer to decide. So we allow anyone to subscribe to `RequestRedraw` and do what they want when they receive the event. The event is nullable as we can't guarantee anyone is subscribed to that event, which will throw a `NullReferenceException`.

Note that all these members are `private`. Consult the documentation (WIP) to see the exposed getters and setters. 

##### Methods and constructor(s)
Because this isn't meant to be a reference per se, I only list the methods that are relevant to explaining the code design.

The constructor creates its own `InputHandler` to get input from. 

`internal void UpdateDirection()` (line 56) gets the first input from the input buffer. The input buffer is a list of `Direction`s and not `System.Windows.Forms.Keys`, and why that is is explained in the implementation details of `Direction`.

`internal bool CheckCollision()` (line 64) checks the snake's head collision with the apple, outer boundaries, or itself. It's used by the UI to determine when to end the game.

`internal void MoveSnake()` (line 118) moves the snake based on `current`. It starts with the last `SnakePoint` in `snakeBody` (the tail of the snake) and it takes on the value of the next `SnakePoint`, and the next `SnakePoint` takes on the value of the next `SnakePoint` and so on until we get to the head. The head's position is incremented based on the increments specified in `directions`. It is mainly used in the autonomous movement when the user doesn't press any key to continue moving in the same direction. Because the default UI calls `MoveSnake()` on a timer, all the logic needs to do to register the keystroke is obtain the last keystroke from `input` and change it, and wait for the next call of `MoveSnake()`.

`internal bool IsCollisionNextTick()` (line 91) is a combination between `CheckCollision()` and `MoveSnake()`; it creates a copy of `snakeBody`, moves it using code similar to that of `MoveSnake()` and use that copy to test for collision. This is used by the grace period feature to determine if we need to pause for an extra tick.

### `InputHandler.cs`
This file defines the class `InputHandler` which translates input passed from a `System.Windows.Form`.

#### `InputHandler`
`InputHandler` is defined like so:
```
internal class InputHandler
{
    // ...
}
```

##### Members and events
`InputHandler` only has one member:
```
// Only handle game controls, nothing else
private List<LogicHandler.Direction> inputBuffer;
```
`inputBuffer` is the input buffer. Notice the comment - `InputHandler` is not designed to handle any input other than any keystrokes relevant to the game logic (like W, A, S and D).

##### Methods and constructor(s)
The constructor initialises `inputBuffer` with one element, `Direction.Right`.

`internal void ProcessInput()` (line 17) 
