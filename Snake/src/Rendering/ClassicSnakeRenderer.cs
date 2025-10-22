// Snake renderer - ClassicSnakeRenderer.cs
// Handles classic mode gameplay graphics for C# graphics
//
// Author: Celeste Ling
// Date:   22/10/25

using System.Drawing.Drawing2D;
using Snake.Logic;

namespace Snake.Rendering
{
    internal class ClassicSnakeRenderer : SnakeRenderer
    {
        internal override float GridSize => 25.0f;
        internal override float AppleSize => 15.0f;
        internal override float SnakeWidth => 12.0f;

        internal readonly float headHeight = 20.0f;
        internal readonly float eyeSize = 6.0f;
        internal readonly float eyeOffset = 3.0f; // from the sides of the head
        SolidBrush snake_hbr = new SolidBrush(Color.White);
        SolidBrush apple_hbr = new SolidBrush(Color.White);
        Pen snake_pen;


        public ClassicSnakeRenderer(ref LogicHandler handler) : base(ref handler)
        {
            snake_pen = new Pen(Color.White, SnakeWidth);
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
            return new PointF(input.X * GridSize, input.Y * GridSize);
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

        internal override void DrawApple(Graphics g)
        {
            RectangleF scr_apple = new RectangleF(SnakePointToPointF(handler.Apple), new SizeF(GridSize, GridSize));
            scr_apple.Inflate(-4, -4);

            g.FillRectangle(apple_hbr, scr_apple);
        }

        internal override void Cleanup()
        {
            snake_hbr.Dispose();
            apple_hbr.Dispose();
            snake_pen.Dispose();
        }
    }
}
