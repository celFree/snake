// Snake game - SnakeRenderer.cs
// Base class for gameplay graphics using WinForms GDI+
//
// Author: Celeste Ling
// Date:   18/09/25

using Snake.Logic;

namespace Snake.Rendering
{
    internal abstract class SnakeRenderer
    {
        protected LogicHandler handler;
        internal abstract float GridSize { get; }
        internal abstract float AppleSize { get; }
        internal abstract float SnakeWidth { get; }


        public SnakeRenderer(ref LogicHandler handler)
        {
            this.handler = handler;
        }

        internal void DrawBackgroundGrid(Bitmap b)
        {
            using (Graphics gb = Graphics.FromImage(b))
            {
                for (int i = 0; i < GameSettings.Default.GridWidth; i++)
                    gb.DrawLine(Pens.LightGray, i * GridSize, 0, i * GridSize, GridSize * GameSettings.Default.GridHeight);

                for (int j = 0; j < GameSettings.Default.GridHeight; j++)
                    gb.DrawLine(Pens.LightGray, 0, j * GridSize, GridSize * GameSettings.Default.GridWidth, j * GridSize);
            }
        }

        internal abstract void DrawBody(Graphics g);
        internal abstract void DrawApple(Graphics g);
        internal abstract void Cleanup();
    }
}
