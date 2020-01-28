using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class MazeA : Maze
    {

        private uint Max_Seed_Count;

        public override void Init(uint h, uint w, int seed = 1)
        {
            if (h < 8) h = 8;
            if (w < 8) w = 8;
            Max_Seed_Count = (h + w) / 4 + 1;
            base.Init(h, w, seed);
        }

        public override void Build()
        {
            uint[] seeds = MakeSeeds();
            while (seeds.Length > 0)
            {
                Expand(seeds);
                seeds = MakeSeeds();
            }
            base.Build();
        }

        private uint[] MakeSeeds()
        {
            List<uint> ret = new List<uint>();
            List<Cell> walls = Cells.Where(c => c.Type == CellType.Wall).ToList();
            foreach(Cell cell in walls)
            {
                uint i = cell.Y * Width + cell.X;
                if (Explore(i).Length > 0) ret.Add(i);
            }
            while (ret.Count > Max_Seed_Count) ret.RemoveAt(Rnd.Next(0, ret.Count));
            return ret.Distinct().ToArray();
        }

        private uint[] Explore(uint idx)
        {
            List<uint> ret = new List<uint>();
            Cell cell = Cells[idx];
            //East
            if (cell.X < Width - 1)
            {
                Cell nextCell = Cells[cell.Y * Width + cell.X + 1];
                if (
                    nextCell.Type == CellType.Lane &&
                    GetCellType((uint)(nextCell.X + 1), (uint)(nextCell.Y)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X + 1), (uint)(nextCell.Y - 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X + 1), (uint)(nextCell.Y + 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X), (uint)(nextCell.Y - 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X), (uint)(nextCell.Y + 1)) == CellType.Lane
                    )
                    ret.Add((uint)(nextCell.Y * Width + nextCell.X));
            }
            //West
            if (cell.X > 0)
            {
                Cell nextCell = Cells[cell.Y * Width + cell.X - 1];
                if (
                    nextCell.Type == CellType.Lane &&
                    GetCellType((uint)(nextCell.X - 1), (uint)(nextCell.Y)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X - 1), (uint)(nextCell.Y - 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X - 1), (uint)(nextCell.Y + 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X), (uint)(nextCell.Y - 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X), (uint)(nextCell.Y + 1)) == CellType.Lane
                    )
                    ret.Add((uint)(nextCell.Y * Width + nextCell.X));
            }
            //North
            if (cell.Y > 0)
            {
                Cell nextCell = Cells[(cell.Y - 1) * Width + cell.X];
                if (
                    nextCell.Type == CellType.Lane &&
                    GetCellType((uint)(nextCell.X - 1), (uint)(nextCell.Y)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X - 1), (uint)(nextCell.Y - 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X), (uint)(nextCell.Y - 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X + 1), (uint)(nextCell.Y - 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X + 1), (uint)(nextCell.Y)) == CellType.Lane
                    )
                    ret.Add((uint)(nextCell.Y * Width + nextCell.X));
            }
            //South
            if (cell.Y < Height - 1)
            {
                Cell nextCell = Cells[(cell.Y + 1) * Width + cell.X];
                if (
                    nextCell.Type == CellType.Lane &&
                    GetCellType((uint)(nextCell.X - 1), (uint)(nextCell.Y)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X - 1), (uint)(nextCell.Y + 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X), (uint)(nextCell.Y + 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X + 1), (uint)(nextCell.Y + 1)) == CellType.Lane &&
                    GetCellType((uint)(nextCell.X + 1), (uint)(nextCell.Y)) == CellType.Lane
                    )
                    ret.Add((uint)(nextCell.Y * Width + nextCell.X));
            }
            return ret.Distinct().ToArray();
        }

        private void Expand(uint[] seeds)
        {
            List<uint> nextSeeds = new List<uint>();
            while (seeds.Length > 0)
            {
                nextSeeds.Clear();
                foreach (uint idx in seeds)
                {
                    uint[] candidates = Explore(idx);
                    if (candidates.Length > 0)
                    {
                        uint newIdx = candidates[Rnd.Next(0, candidates.Length)];
                        Cells[newIdx].Type = CellType.Wall;
                        nextSeeds.Add(newIdx);
                    }
                }
                if (Fun_onExpand != null) Fun_onExpand.Invoke();
                seeds = nextSeeds.Distinct().ToArray();
            }
        }
    }
}
