// TODO: Decouple from Form1

// Snake game - Form1.Logic.cs
// Handles logic related to snake movement
//
// Author: celFree
// Date:   17/09/25

namespace Snake.Logic
{
    public record struct SnakePoint(int X, int Y);

    internal class LogicHandler
    {
        // Snake object
        // Change snake starting location here
        List<SnakePoint> snakeBody = [new SnakePoint(5, 8), new SnakePoint(4, 8)];
        
        // Apple object
        // Change apple starting location here
        // I have no idea why I call the snake food apples, I've played too much Google Snake
        SnakePoint apple = new SnakePoint(11, 8);

        private int score = 0;

        // Directional shit
        public enum Direction
        {
            Up, Down, Left, Right
        }

        Dictionary<Direction, SnakePoint> directions = new Dictionary<Direction, SnakePoint>()
        {
            { Direction.Up, new SnakePoint(0, -1) },
            { Direction.Down, new SnakePoint(0, 1) },
            { Direction.Left, new SnakePoint(-1, 0) },
            { Direction.Right, new SnakePoint(1, 0) }
        };

        Direction current = Direction.Right;
        bool paused = false;

        InputHandler input;

        // Other shit
        Random r = new Random();

        // Events
        public event EventHandler? RequestRedraw;

        public LogicHandler()
        {
            input = new InputHandler();
        }

        internal void UpdateDirection()
        {
            if (input.InputBuffer.Count() != 0)
            {
                current = input.InputBuffer.First();
            }
        }

        internal bool CheckCollision()
        {
            SnakePoint tail = snakeBody.Last();

            if (snakeBody.Skip(1).Any(pt => pt.Equals(snakeBody[0])))
            {
                RequestRedraw?.Invoke(this, EventArgs.Empty);
                // GameOverProc("You ate yourself bozo\nsnake sez: owie");
                return true;
            }

            if (snakeBody[0].X < 0 || snakeBody[0].X > GameSettings.Default.GridWidth || snakeBody[0].Y < 0 || snakeBody[0].Y > GameSettings.Default.GridHeight)
            {
                return true;
                // GameOverProc("You crashed bozo\nsnake sez: owie");
            }

            if (snakeBody[0].Equals(apple))
            {
                snakeBody.Add(tail);
                score++;
                PlaceApple();
            }

            return false;
        }

        internal bool IsCollisionNextTick()
        {
            SnakePoint[] snakeBodyTemp = snakeBody.ToArray(); // remember, Point is value type

            for (int i = snakeBodyTemp.Length - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    snakeBodyTemp[0] = new SnakePoint(snakeBodyTemp[0].X + directions[current].X, snakeBodyTemp[0].Y + directions[current].Y);
                }
                else
                    snakeBodyTemp[i] = snakeBodyTemp[i - 1];
            }

            if (snakeBodyTemp.Skip(1).Any(pt => pt.Equals(snakeBodyTemp[0])))
            {
                return true;
            }

            if (snakeBodyTemp[0].X < 0 || snakeBodyTemp[0].X > GameSettings.Default.GridWidth || snakeBodyTemp[0].Y < 0 || snakeBodyTemp[0].Y > GameSettings.Default.GridHeight)
            {
                return true;
            }

            return false;
        }

        internal void MoveSnake()
        {
            for (int i = snakeBody.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    snakeBody[0] = new SnakePoint(snakeBody[0].X + directions[current].X, snakeBody[0].Y + directions[current].Y);
                }
                else
                    snakeBody[i] = snakeBody[i - 1];
            }
        }

        private void PlaceApple()
        {
            while (true)
            {
                apple.X = r.Next(GameSettings.Default.GridWidth);
                apple.Y = r.Next(GameSettings.Default.GridHeight);

                if (snakeBody.Any(pt => pt.Equals(apple)))
                    continue;

                break;
            }
        }

        internal void ResetGame()
        {
            snakeBody.Clear();
            snakeBody = [new SnakePoint(5, 8), new SnakePoint(4, 8)];

            apple = new SnakePoint(11, 8);
            input.ClearBuffer();
            current = Direction.Right;
            score = 0;
            RequestRedraw?.Invoke(this, EventArgs.Empty);
        }

        public SnakePoint[] SnakeBody
        {
            get { return snakeBody.ToArray(); }
        }

        public SnakePoint Apple
        {
            get { return apple; }
        }

        public Direction CurrentDirection
        {
            get { return this.current; }
        }

        public bool Paused
        {
            get { return this.paused; }
        }

        public InputHandler InputHandler
        {
            get { return this.input; }
        }

        public int Score
        {
            get { return this.score; }
        }
    }
}
