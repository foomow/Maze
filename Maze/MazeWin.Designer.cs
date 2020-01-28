namespace Maze
{
    partial class MazeWin
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
            this.MazePicBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Size_CBX = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rb_mazeA = new System.Windows.Forms.RadioButton();
            this.rb_mazeB = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.MazePicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MazePicBox
            // 
            this.MazePicBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MazePicBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MazePicBox.Location = new System.Drawing.Point(13, 13);
            this.MazePicBox.Name = "MazePicBox";
            this.MazePicBox.Size = new System.Drawing.Size(600, 600);
            this.MazePicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MazePicBox.TabIndex = 0;
            this.MazePicBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(727, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Build";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Size_CBX
            // 
            this.Size_CBX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Size_CBX.FormattingEnabled = true;
            this.Size_CBX.Items.AddRange(new object[] {
            "8",
            "16",
            "32",
            "48",
            "64",
            "72",
            "96",
            "128",
            "144",
            "196",
            "256",
            "512"});
            this.Size_CBX.Location = new System.Drawing.Point(681, 65);
            this.Size_CBX.Name = "Size_CBX";
            this.Size_CBX.Size = new System.Drawing.Size(121, 21);
            this.Size_CBX.TabIndex = 2;
            this.Size_CBX.Text = "8";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(726, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Walkout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(726, 152);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Animation";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(619, 216);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(183, 397);
            this.textBox1.TabIndex = 5;
            // 
            // rb_mazeA
            // 
            this.rb_mazeA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_mazeA.AutoSize = true;
            this.rb_mazeA.Checked = true;
            this.rb_mazeA.Location = new System.Drawing.Point(716, 13);
            this.rb_mazeA.Name = "rb_mazeA";
            this.rb_mazeA.Size = new System.Drawing.Size(58, 17);
            this.rb_mazeA.TabIndex = 6;
            this.rb_mazeA.TabStop = true;
            this.rb_mazeA.Text = "MazeA";
            this.rb_mazeA.UseVisualStyleBackColor = true;
            // 
            // rb_mazeB
            // 
            this.rb_mazeB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rb_mazeB.AutoSize = true;
            this.rb_mazeB.Location = new System.Drawing.Point(717, 36);
            this.rb_mazeB.Name = "rb_mazeB";
            this.rb_mazeB.Size = new System.Drawing.Size(58, 17);
            this.rb_mazeB.TabIndex = 7;
            this.rb_mazeB.Text = "MazeB";
            this.rb_mazeB.UseVisualStyleBackColor = true;
            // 
            // MazeWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 643);
            this.Controls.Add(this.rb_mazeB);
            this.Controls.Add(this.rb_mazeA);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Size_CBX);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MazePicBox);
            this.Name = "MazeWin";
            this.Text = "MazeWin";
            this.Load += new System.EventHandler(this.MazeWin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MazePicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MazePicBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Size_CBX;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rb_mazeA;
        private System.Windows.Forms.RadioButton rb_mazeB;
    }
}

