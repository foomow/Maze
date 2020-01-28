using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    abstract class Maze : IMaze
    {
        private Stack<uint> PathStack;
        private List<uint> Passed;
        private uint[] Path;
        private uint EndIdx;
        protected Random Rnd;

        public Action Fun_onExpand { get; set; }
        public Action<uint, bool> Fun_onStep { get; set; }
        public Action<uint, bool> Fun_onBack { get; set; }
        public Cell[] Cells { get; set; }
        public uint Height { get; set; }
        public uint Width { get; set; }

        public virtual void Build()
        {
            Cells[1].Type = CellType.Lane;
            Cells[Height * Width - 2].Type = CellType.Lane;
        }

        public virtual void Init(uint h, uint w, int seed = 1)
        {
            Rnd = new Random(DateTime.Now.Millisecond * seed);
            if (h < 8) h = 8;
            if (w < 8) w = 8;
            Height = h;
            Width = w;
            Cells = new Cell[h * w];
            for (uint y = 0; y < Height; y++)
            {
                for (uint x = 0; x < Width; x++)
                    Cells[y * Width + x] = new Cell() { ToStart = w * h, X = x, Y = y, Type = (x == 0 || y == 0 || x == Width - 1 || y == Height - 1) ? CellType.Wall : CellType.Lane };
            }
            Build();
        }

        public virtual uint[] GetPath(uint startIdx, uint endIdx)
        {
            Path = null;
            Passed = new List<uint>();
            EndIdx = endIdx;
            PathStack = new Stack<uint>();
            for (uint idx = 0; idx < Cells.Length; idx++)
                Cells[idx].ToStart = Width * Height;
            StepForward(startIdx);
            return Path;
        }

        protected virtual void StepForward(uint idx, Direction dir = Direction.East)
        {
            PathStack.Push(idx);
            if (Fun_onStep != null) Fun_onStep.Invoke(idx, true);
            if (!(
                PathStack.Count >= Cells[idx].ToStart
                || (Path != null && PathStack.Count >= Path.Length)
                )
                )

            {
                Cells[idx].ToStart = (uint)PathStack.Count;

                if (idx == EndIdx)
                {
                    Passed.Clear();
                    Path = PathStack.ToArray();
                }
                else
                {
                    if (!Passed.Contains(idx)) Passed.Add(idx);
                    List<Step> nextStep = new List<Step>();
                    if (GetCellType(Cells[idx].X + 1, Cells[idx].Y) == CellType.Lane)
                    {
                        if (!Passed.Contains(idx + 1) && !CheckCut(idx + 1, idx)
                            ) nextStep.Add(new Step() { idx = idx + 1, dir = Direction.East });
                    }
                    if (GetCellType(Cells[idx].X - 1, Cells[idx].Y) == CellType.Lane)
                    {
                        if (!Passed.Contains(idx - 1) && !CheckCut(idx - 1, idx)
                            ) nextStep.Add(new Step() { idx = idx - 1, dir = Direction.West });
                    }
                    if (GetCellType(Cells[idx].X, Cells[idx].Y + 1) == CellType.Lane)
                    {
                        if (!Passed.Contains(idx + Width) && !CheckCut(idx + Width, idx)
                            ) nextStep.Add(new Step() { idx = idx + Width, dir = Direction.South });
                    }
                    if (GetCellType(Cells[idx].X, Cells[idx].Y - 1) == CellType.Lane)
                    {
                        if (!Passed.Contains(idx - Width) && !CheckCut(idx - Width, idx)
                            ) nextStep.Add(new Step() { idx = idx - Width, dir = Direction.North });
                    }
                    int samediridx = nextStep.FindIndex(x => x.dir == dir);
                    if (samediridx > -1)
                    {
                        Step samedirstep = nextStep[samediridx];
                        nextStep.RemoveAt(samediridx);
                        nextStep.Insert(0, samedirstep);
                    }

                    foreach (Step newdir in nextStep)
                    {
                        StepForward(newdir.idx, newdir.dir);
                    }
                }

            }
            uint backidx = PathStack.Pop();
            if (Fun_onBack != null)
            {
                if (Path == null || !Path.Contains(idx))
                    Fun_onBack.Invoke(idx, true);
            }
        }

        protected virtual bool CheckCut(uint idx, uint fromidx)
        {
            Cell cell = Cells[idx];
            if (GetCellType(cell.X + 1, cell.Y) == CellType.Lane)
            {
                uint checkIdx = cell.Y * Width + cell.X + 1;
                if (fromidx != checkIdx && PathStack.Contains(checkIdx)) return true;
            }
            if (GetCellType(cell.X - 1, cell.Y) == CellType.Lane)
            {
                uint checkIdx = cell.Y * Width + cell.X - 1;
                if (fromidx != checkIdx && PathStack.Contains(checkIdx)) return true;
            }
            if (GetCellType(cell.X, cell.Y + 1) == CellType.Lane)
            {
                uint checkIdx = (cell.Y + 1) * Width + cell.X;
                if (fromidx != checkIdx && PathStack.Contains(checkIdx)) return true;
            }
            if (GetCellType(cell.X, cell.Y - 1) == CellType.Lane)
            {
                uint checkIdx = (cell.Y - 1) * Width + cell.X;
                if (fromidx != checkIdx && PathStack.Contains(checkIdx)) return true;
            }
            return false;
        }

        protected CellType GetCellType(uint x, uint y)
        {
            CellType ret = CellType.Wall;
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return Cells[y * Width + x].Type;
            }
            return ret;
        }
    }
}
