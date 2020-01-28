using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze
{
    public partial class MazeWin : Form
    {
        private IMaze maze;
        private ushort size = 16;
        private int pixelSize = 8;
        private Bitmap bitmap;
        private Bitmap image;

        public Graphics Graph;

        private delegate void UpdatePicFun();
        private delegate void UpdateUIFun(bool enable);
        private delegate void UpdateTextFun(string msg);
        private UpdatePicFun updatePicFun;
        private UpdateUIFun updateUIFun;
        private UpdateTextFun updateTextFun;

        public MazeWin()
        {
            InitializeComponent();
        }

        private void MazeWin_Load(object sender, EventArgs e)
        {
            updatePicFun = UpdatePic;
            updateUIFun = UpdateUI;
            updateTextFun = UpdateText;
        }

        private void UpdateText(string msg)
        {
            textBox1.AppendText(msg + "\n");
        }

        private void UpdateUI(bool enable)
        {
            button1.Enabled = enable;
            button2.Enabled = enable;
            Size_CBX.Enabled = enable;
            checkBox1.Enabled = enable;
        }

        private void WriteLine(string msg)
        {
            BeginInvoke(updateTextFun, msg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BeginInvoke(updateUIFun, false);
            WriteLine($"===== Creating path");
            Draw();
            GC.Collect();
            new Thread(new ThreadStart(() =>
            {
                DateTime t1 = DateTime.Now;
                uint[] path = maze.GetPath(1, maze.Height * maze.Width - 2);
                WriteLine($"===== Path is created in: {(DateTime.Now - t1).TotalMilliseconds} ms");
                DrawPath(path);
                BeginInvoke(updateUIFun, true);
            }), 1024 * 1024 * 20).Start();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            BeginInvoke(updateUIFun, false);
            WriteLine($"===== Creating maze");
            if (!ushort.TryParse(Size_CBX.Text.Trim(), out size))
            {
                size = 8;
            }
            if (size < 8) size = 8;

            Collect();

            if (rb_mazeA.Checked)
                maze = new MazeA();
            if (rb_mazeB.Checked)
                maze = new MazeB();

            if (checkBox1.Checked)
            {
                maze.Fun_onExpand = Draw;
                maze.Fun_onStep = DrawStep;
                maze.Fun_onBack = DrawCell;
            }
            else
            {
                maze.Fun_onExpand = null;
                maze.Fun_onStep = null;
                maze.Fun_onBack = null;
            }


            bitmap = new Bitmap(size * pixelSize, size * pixelSize);
            image = Image.FromHbitmap(bitmap.GetHbitmap());
            Graph = Graphics.FromImage(image);

            new Thread(new ThreadStart(() =>
            {
                DateTime t1 = DateTime.Now;
                maze.Init(size, size);
                WriteLine($"===== Maze is created in: {(DateTime.Now - t1).TotalMilliseconds} ms");
                Draw();
                GC.Collect();
                BeginInvoke(updateUIFun, true);
            }), 1024 * 1024 * 20).Start();
        }
        private void Collect()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
            if (Graph != null)
            {
                Graph.Dispose();
                Graph = null;
            }
            if (maze != null)
            {
                maze = null;
            }

            GC.Collect();
        }
        private void Draw()
        {
            for (uint idx = 0; idx < maze.Cells.Length; idx++)
            {
                DrawCell(idx);
            }
            BeginInvoke(updatePicFun);
        }

        private void DrawPath(uint[] path)
        {
            if (path != null)
            {
                foreach (uint idx in path)
                {
                    DrawStep(idx);
                }
                BeginInvoke(updatePicFun);
            }
        }

        private void DrawStep(uint idx, bool redraw = false)
        {
            Cell cell = maze.Cells[idx];
            Brush brush = Brushes.Red;
            Graph.FillRectangle(brush, cell.X * pixelSize + 2, cell.Y * pixelSize + 2, pixelSize - 5, pixelSize - 5);
            if (redraw)
            {
                Thread.Sleep(1);
                BeginInvoke(updatePicFun);
            }
        }

        private void DrawCell(uint idx, bool redraw = false)
        {
            Cell cell = maze.Cells[idx];
            Brush brush = cell.Type == CellType.Wall ? Brushes.White : Brushes.Black;
            Graph.FillRectangle(brush, cell.X * pixelSize, cell.Y * pixelSize, pixelSize - 1, pixelSize - 1);
            if (redraw)
            {
                BeginInvoke(updatePicFun);
            }
        }

        private void UpdatePic()
        {
            MazePicBox.Image = image;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (maze != null)
            {
                if (((CheckBox)sender).Checked)
                {
                    maze.Fun_onExpand = Draw;
                    maze.Fun_onStep = DrawStep;
                    maze.Fun_onBack = DrawCell;
                }
                else
                {
                    maze.Fun_onExpand = null;
                    maze.Fun_onStep = null;
                    maze.Fun_onBack = null;
                }
            }
        }
    }
}
