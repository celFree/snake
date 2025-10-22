// Snake rendering - ModernSnakeRenderer.cs
// Handles modern mode gameplay graphics for C# graphics
//
// Author: Celeste Ling
// Date:   22/10/25

using System.Drawing.Drawing2D;
using Snake.Logic;

namespace Snake.Rendering
{
    internal class ModernSnakeRenderer : SnakeRenderer
    {
        internal override float GridSize => 25.0f;
        internal override float AppleSize => 15.0f;
        internal override float SnakeWidth => 12.0f;

        internal readonly float headHeight = 20.0f;
        internal readonly float eyeSize = 6.0f;
        internal readonly float eyeOffset = 3.0f; // from the sides of the head
        SolidBrush snake_hbr = new SolidBrush(Color.LightGreen);
        SolidBrush apple_hbr = new SolidBrush(Color.Red);
        SolidBrush leaf_hbr = new SolidBrush(Color.Green);
        SolidBrush text_hbr = new SolidBrush(Color.White);
        Pen snake_pen;
        

        public ModernSnakeRenderer(ref LogicHandler handler) : base(ref handler)
        {
            snake_pen = new Pen(Color.LightGreen, SnakeWidth);
            snake_pen.StartCap = LineCap.Flat;
            snake_pen.EndCap = LineCap.Round;
            snake_pen.LineJoin = LineJoin.Round;
        }

        public PointF[] SnakePointToPointF(SnakePoint[] input)
        {
            PointF[] ret = new PointF[input.Length];
            
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i].X = input[i].X * GridSize;
                ret[i].Y = input[i].Y * GridSize;
            }

            return ret;
        }

        public PointF SnakePointToPointF(SnakePoint input)
        {
            return new PointF(input.X *  GridSize, input.Y * GridSize);
        }

        internal override void DrawBody(Graphics g)
        {
            PointF[] snakeBody = SnakePointToPointF(handler.SnakeBody);

            using (GraphicsPath gp = new GraphicsPath())
            {
                PointF[] points = snakeBody.Select(p => new PointF(p.X + GridSize / 2, p.Y + GridSize / 2)).ToArray();
                gp.AddLines(points);

                g.DrawPath(snake_pen, gp);
            }
        }

        internal void DrawHead(Graphics g)
        {
            PointF[] snakeBody = SnakePointToPointF(handler.SnakeBody);

            // Draw head
            RectangleF head = new RectangleF();
            if (handler.CurrentDirection == LogicHandler.Direction.Up || handler.CurrentDirection == LogicHandler.Direction.Down)
            {
                head.X = snakeBody[0].X + (GridSize - headHeight) / 2;
                head.Y = snakeBody[0].Y;
                head.Width = headHeight;
                head.Height = GridSize;
            }
            else
            {
                head.X = snakeBody[0].X;
                head.Y = snakeBody[0].Y + (GridSize - headHeight) / 2;
                head.Width = GridSize;
                head.Height = headHeight;
            }

            g.FillRectangle(snake_hbr, head);
        }

        internal void DrawEyes(Graphics g)
        {
            PointF[] snakeBody = SnakePointToPointF(handler.SnakeBody);

            // Draw eyes
            using (SolidBrush eye_hbr = new SolidBrush(Color.Black))
            {
                RectangleF eye1 = new RectangleF(0, 0, eyeSize, eyeSize);
                RectangleF eye2 = new RectangleF(0, 0, eyeSize, eyeSize);
                switch (handler.CurrentDirection)
                {
                    case LogicHandler.Direction.Up:
                        {
                            eye1.X = eyeOffset;
                            eye2.X = headHeight - eyeOffset - eyeSize;
                            eye1.Y = eyeOffset;
                            eye2.Y = eyeOffset;

                            break;
                        }
                    case LogicHandler.Direction.Down:
                        {
                            eye1.X = eyeOffset;
                            eye2.X = headHeight - eyeOffset - eyeSize;
                            eye1.Y = GridSize - eyeOffset - eyeSize;
                            eye2.Y = GridSize - eyeOffset - eyeSize;
                            break;
                        }
                    case LogicHandler.Direction.Left:
                        {
                            eye1.X = eyeOffset;
                            eye2.X = eyeOffset;
                            eye1.Y = eyeOffset;
                            eye2.Y = headHeight - eyeOffset - eyeSize;
                            break;
                        }
                    case LogicHandler.Direction.Right:
                        {
                            eye1.X = GridSize - eyeOffset - eyeSize;
                            eye2.X = GridSize - eyeOffset - eyeSize;
                            eye1.Y = headHeight - eyeOffset - eyeSize;
                            eye2.Y = eyeOffset;
                            break;
                        }
                }

                eye1.Offset(snakeBody[0]);
                eye2.Offset(snakeBody[0]);

                g.FillRectangle(eye_hbr, eye1);
                g.FillRectangle(eye_hbr, eye2);
            }
        }

        internal override void DrawApple(Graphics g)
        {
            RectangleF scr_apple = new RectangleF(SnakePointToPointF(handler.Apple), new SizeF(GridSize, GridSize));
            scr_apple.Inflate(-4, -4);
            scr_apple.Offset(0, 4);

            RectangleF leaf = new RectangleF(handler.Apple.X * GridSize + 14, handler.Apple.Y * GridSize + 2, 10, 10);

            g.FillEllipse(apple_hbr, scr_apple);
            g.FillEllipse(leaf_hbr, leaf);
        }

        internal override void Cleanup()
        {
            snake_hbr.Dispose();
            apple_hbr.Dispose();
            leaf_hbr.Dispose();
            text_hbr.Dispose();
            snake_pen.Dispose();
        }
    }
}
