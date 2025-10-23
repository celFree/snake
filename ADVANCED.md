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
This is a pretty important class, which is why I decided to split the LogicHandler.cs section into multiple sub-sections.

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

private int score = 0;

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
+ `current` represents which `Direction` the snake's head is facing.
+ `input` is an instance of `InputHandler`. The specifics of `InputHandler` will be explained later, just know that `LogicHandler` grabs inputs already translated to `Direction`s and moves accordingly.
+ `r` is an instance of `Random`. It is used to randomly generate the coordinates of the object.
+ `RequestRedraw` is an nullable event that is raised when the logic handler wants a renderer to redraw. Why not use `Control.Invalidate()` then? The problem is that then makes the logic dependent on `System.Windows.Forms` which we can't guarantee that the renderer or the UI uses. Additionally, it forces the relevant renderers to redraw, when we shouldn't concern ourselves on renderer details. We shouldn't care if the renderer redraws or not; that is up to the renderer to decide. So we allow anyone to subscribe to `RequestRedraw` and do what they want when they receive the event. The event is nullable as we can't guarantee anyone is subscribed to that event, which will throw a `NullReferenceException`.

#### Methods and constructor(s)
| Name | Access modifier | Arguments | Return type | Summary |
|---|---|---|---|---|
`LogicHandler` | `public`   | none | none   | Initialises a new instance of `LogicHandler`. This also creates initialises its own input buffer.
`UpdateDirection` | `internal` | none | `void` | Sets the current direction (`current`) to the first element in the input buffer.
`CheckCollision` | `internal` | none | `bool` | Checks for collision with the snake's head and the outer boundaries, apple or itself.
`IsCollisionNextTick` | `internal` | none | `bool` | Creates a copy of the snake body, moves the temporary snake based on the current direction and checking for a collision. This method is used for the grace period feature.
`MoveSnake` | `internal` | none | `void` | Moves the snake based on the current direction. 
`PlaceApple` | `private` | none | `void` | Randomly place the apple on the anywhere on the field except where the snake's body is.
`ResetGame` | `internal` | none | `void` | Resets the game - sets the snake to its starting length and position, sets the apple in its default starting position and resets the score to 0.

#### Getters and setters
| Name | Access modifier | Read only? | Summary |
|---|---|---|---|
