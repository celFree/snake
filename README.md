# snake
A version of the classic Snake game written in C# and WinForms. 

## Features
### Comfortable gameplay
Many basic Snake implementations will draw the snake as mutliple shapes, which not only makes it hard to see which direction the snake is facing, where the snake's head is, etc, but it just looks plain ugly and like a blob on the screen.

This implementation draws the snake as a line, which not only makes it easy to see what direction the snake is facing, but it actually looks like a snake, and is much faster than drawing multiple shapes.

An input buffer is used to keep track of the user's inputs. This makes the game more responsive and makes sure no input is lost.

When you are about to game over, the game gives you a grace period to allow you to quickly turn away and save your run.

### Customisation
You can change:
+ Field width and height
+ Tick interval
+ Graphics mode
+ Game scale

You can make your graphics mode by simply making a C# script and implementing the `SnakeRenderer` interface.

## Why did I make this?
Yes, Snake implementations miles ahead of my version do exist. 

I wrote this a while ago during my free time in between classes, and it's been sitting in my repos folder for a little over a month or so. However, I had a lot of fun making this and learning about the C# drawing library. My goal of this project was to make a minimal base implementation of Snake with some comfort features and high customisability. Yeah, I haven't reached these goals 100% but I am working towards it. 

You can choose to play this or not, you have free will. Browse the code or not, I don't mind (but if you do, thanks <3). The only guarantee I have is that this implementation of Snake is functional, and I hope you have as much fun with it as I did.
