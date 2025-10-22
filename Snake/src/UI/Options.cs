using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake.UI
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        public event EventHandler? OptionsChanged;

        // OK button
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked) // classic 
                GameSettings.Default.VisualMode = 1;
            else // modern
                GameSettings.Default.VisualMode = 2;

            GameSettings.Default.GridWidth = (int)numericUpDown1.Value;
            GameSettings.Default.GridHeight = (int)numericUpDown2.Value;
            GameSettings.Default.GridSize = (int)numericUpDown4.Value;
            GameSettings.Default.UpdateInterval = (int)numericUpDown3.Value;
            GameSettings.Default.Save();
            OptionsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void Options_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Options_Shown(object sender, EventArgs e)
        {
            if(GameSettings.Default.VisualMode == 1)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
            else
            {
                radioButton2.Checked = false;
                radioButton1.Checked = true;
            }

            numericUpDown1.Value = GameSettings.Default.GridWidth;
            numericUpDown2.Value = GameSettings.Default.GridHeight;
            numericUpDown4.Value = GameSettings.Default.GridSize;
            numericUpDown3.Value = GameSettings.Default.UpdateInterval;
        }
    }
}
