namespace Snake.UI
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            groupBox2 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            numericUpDown4 = new NumericUpDown();
            label1 = new Label();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            groupBox3 = new GroupBox();
            label2 = new Label();
            numericUpDown3 = new NumericUpDown();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(295, 92);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Visuals";
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Checked = true;
            radioButton2.Location = new Point(17, 26);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(74, 24);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "Classic";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(17, 56);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(86, 24);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Modern ";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(numericUpDown4);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(numericUpDown2);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Location = new Point(12, 110);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(295, 92);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Field size";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(249, 61);
            label4.Name = "label4";
            label4.Size = new Size(25, 20);
            label4.TabIndex = 5;
            label4.Text = "px";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 61);
            label3.Name = "label3";
            label3.Size = new Size(106, 20);
            label3.TabIndex = 4;
            label3.Text = "each square is ";
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(129, 59);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(114, 27);
            numericUpDown4.TabIndex = 3;
            numericUpDown4.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(140, 28);
            label1.Name = "label1";
            label1.Size = new Size(16, 20);
            label1.TabIndex = 2;
            label1.Text = "x";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(162, 26);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(114, 27);
            numericUpDown2.TabIndex = 1;
            numericUpDown2.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(18, 26);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(114, 27);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(numericUpDown3);
            groupBox3.Location = new Point(12, 208);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(295, 69);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Movement interval";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(137, 28);
            label2.Name = "label2";
            label2.Size = new Size(28, 20);
            label2.TabIndex = 3;
            label2.Text = "ms";
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(17, 26);
            numericUpDown3.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(114, 27);
            numericUpDown3.TabIndex = 3;
            numericUpDown3.Value = new decimal(new int[] { 250, 0, 0, 0 });
            // 
            // button1
            // 
            button1.Location = new Point(216, 287);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 4;
            button1.Text = "Cancel";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(116, 287);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 5;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(16, 287);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 6;
            button3.Text = "Reset";
            button3.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            AcceptButton = button2;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button1;
            ClientSize = new Size(322, 328);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Options";
            Text = "Options";
            FormClosing += Options_FormClosing;
            Shown += Options_Shown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private GroupBox groupBox2;
        private Label label1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private GroupBox groupBox3;
        private Label label2;
        private NumericUpDown numericUpDown3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label4;
        private Label label3;
        private NumericUpDown numericUpDown4;
    }
}