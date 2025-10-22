// Snake game - Form1.Input.cs
// Handles all user input including the input buffer
//
// Author: Celeste Ling
// Date:   17/09/25

namespace Snake.Logic
{
    internal partial class InputHandler
    {
        // Only handle game controls, nothing else
        private List<LogicHandler.Direction> inputBuffer;
        internal InputHandler() 
        { 
            this.inputBuffer = new List<LogicHandler.Direction>() { LogicHandler.Direction.Right };
        }
        internal void ProcessInput(KeyEventArgs e, LogicHandler.Direction current)
        {
            LogicHandler.Direction check;

            // the key to check against
            if (inputBuffer.Count > 0)
                check = inputBuffer.First();
            else
                check = current;

            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    if (check == LogicHandler.Direction.Down || check == LogicHandler.Direction.Up)
                        return;

                    inputBuffer.Add(LogicHandler.Direction.Up);
                    break;

                case Keys.A:
                case Keys.Left:
                    if (check == LogicHandler.Direction.Right || check == LogicHandler.Direction.Left)
                        return;

                    inputBuffer.Add(LogicHandler.Direction.Left);
                    break;

                case Keys.S:
                case Keys.Down:
                    if (check == LogicHandler.Direction.Up || check == LogicHandler.Direction.Down)
                        return;

                    inputBuffer.Add(LogicHandler.Direction.Down);
                    break;

                case Keys.D:
                case Keys.Right:
                    if (check == LogicHandler.Direction.Left || check == LogicHandler.Direction.Right)
                        return;

                    inputBuffer.Add(LogicHandler.Direction.Right);
                    break;
            }
        }

        internal List<LogicHandler.Direction> InputBuffer
        {
            get { return inputBuffer; }
        }

        internal void PopInputBuffer()
        {
            if (inputBuffer.Count > 0)
                inputBuffer.RemoveAt(0);
        }

        internal void ClearBuffer()
        {
            inputBuffer.Clear();
        }
    }
}
