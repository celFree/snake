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

Rendering handles all graphics and acts as a translation layer between logic and what is seen on screen. Because it must access platform specific drawing libraries (in this case, `Graphics) it is not portable.

UI handles most of the graphical initialisation and receives events from the operating system, such as keystrokes and when to terminate. 
