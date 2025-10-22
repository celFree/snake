// Snake game - Form1.cs
// Handles high level logic (mainly event handlers)
//
// Author: Celeste Ling
// Date:   17/09/25

// There's an obnoxious amount of comments here because I actually want to find shit
// And I'm very not fucking sorry for the profane language in the source code.
// Here's why: https://news.ycombinator.com/item?id=34761052

using Snake.Logic;
using Snake.Rendering;
using System.Drawing.Drawing2D;

namespace Snake.UI
{
    public partial class Form1 : Form
    {
        LogicHandler logic;
        InputHandler input;
        SnakeRenderer renderer;

        enum VisualMode
        {
            Classic = 1, Modern = 2
        }

        VisualMode mode = VisualMode.Classic;

        // Options window
        Options optionsForm = new Options();

        public Form1()
        {
            InitializeComponent();
            logic = new LogicHandler();
            input = logic.InputHandler;
            renderer = new ClassicSnakeRenderer(ref logic);
            logic.RequestRedraw += (sender, e) => Invalidate();
            
            optionsForm.OptionsChanged += (sender, e) =>
            {
                mode = (VisualMode)GameSettings.Default.VisualMode;

                if (mode == VisualMode.Classic)
                    renderer = new ClassicSnakeRenderer(ref logic);
                else renderer = new ModernSnakeRenderer(ref logic);

                    ClientSize = new Size(GameSettings.Default.GridWidth * (int)renderer.GridSize, GameSettings.Default.GridHeight * (int)renderer.GridSize);
                timer1.Interval = GameSettings.Default.UpdateInterval;
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(GameSettings.Default.GridWidth * (int)renderer.GridSize, GameSettings.Default.GridHeight * (int)renderer.GridSize);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            // TODO: add this to options pane and because this is a shit way of toggling settings
            // Debug grid - change the if statement to enable or disable
            if (false)
            {
                using (Bitmap b = new Bitmap(GameSettings.Default.GridWidth * (int)renderer.GridSize, GameSettings.Default.GridHeight * (int)renderer.GridSize))
                {
                    renderer.DrawBackgroundGrid(b);
                    g.DrawImage(b, 0, 0);
                }
            }

            renderer.DrawApple(g);
            renderer.DrawBody(g);

            if(mode == VisualMode.Modern)
            {
                ((ModernSnakeRenderer)renderer).DrawHead(g);
                ((ModernSnakeRenderer)renderer).DrawEyes(g);
            } 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.O)
            {
                optionsForm.Show();
            }

            if (!timer1.Enabled && e.KeyCode == Keys.W | e.KeyCode == Keys.A | e.KeyCode == Keys.S | e.KeyCode == Keys.D | e.KeyCode == Keys.Up | e.KeyCode == Keys.Down | e.KeyCode == Keys.Left | e.KeyCode == Keys.Right)
                timer1.Start();

            input.ProcessInput(e, logic.CurrentDirection);
            Invalidate();
        }

        // If user has been given grace period
        bool grace = false;

        // Main game loop 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (logic.IsCollisionNextTick() && !grace)
            {
                grace = true;
                return;
            }

            grace = false;

            logic.UpdateDirection();
            input.PopInputBuffer();
            logic.MoveSnake();

            if(logic.CheckCollision())
            {
                timer1.Stop();
                MessageBox.Show($"You lose bozo\n\nFinal score: {logic.Score}", "Game over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logic.ResetGame();
            }

            Invalidate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            renderer.Cleanup();
            base.OnFormClosing(e);
        }
    }
}
