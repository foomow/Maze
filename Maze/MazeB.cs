using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class MazeB : Maze
    {
        private List<uint> ConnectedAreas;
        public override void Build()
        {
            ConnectedAreas = new List<uint>() { 0 };
            for (uint col = 2; col < Width - 2; col += 2)
            {
                for (uint row = 1; row < Height - 1; row++)
                {
                    Cells[row * Width + col].Type = CellType.Wall;
                }
            }
            for (uint row = 2; row < Height - 2; row += 2)
            {
                for (uint col = 1; col < Width - 1; col++)
                {
                    Cells[row * Width + col].Type = CellType.Wall;
                }
            }
            for (uint i = 0; i < Cells.Length; i++)
            {
                if (Cells[i].Type == CellType.Lane) Cells[i].ToStart = i;
            }
            Cells[1].Type = CellType.Lane;
            Cells[1].ToStart = 0;
            Cells[Height * Width - 2].Type = CellType.Lane;

            List<Cell> unconnectedList = Cells.Where(x => ConnectedAreas.Contains(x.ToStart) && x.Type == CellType.Lane && CheckBoundry(x)).ToList();
            Direction dir;
            while (unconnectedList.Count > 0)
            {
                foreach (Cell cell in unconnectedList)
                {
                    //if (Cells[cell.Y * Width + cell.X].ToStart > 0)
                    {
                        dir = (Direction)Rnd.Next(0, 4);
                        Expand(cell, dir);
                    }
                }
                if (Fun_onExpand != null)
                    Fun_onExpand.Invoke();
                unconnectedList = Cells.Where(x => ConnectedAreas.Contains(x.ToStart) && x.Type == CellType.Lane && CheckBoundry(x)).ToList();
            }

        }

        private bool CheckBoundry(Cell cell)
        {
            if (GetCellType(cell.X + 1, cell.Y) == CellType.Lane &&  !ConnectedAreas.Contains(Cells[cell.Y * Width + cell.X + 1].ToStart)) return true;
            if (GetCellType(cell.X + 1, cell.Y) == CellType.Wall && GetCellType(cell.X + 2, cell.Y) == CellType.Lane && !ConnectedAreas.Contains(Cells[cell.Y * Width + cell.X + 2].ToStart)) return true;
            if (GetCellType(cell.X - 1, cell.Y) == CellType.Lane && !ConnectedAreas.Contains(Cells[cell.Y * Width + cell.X - 1].ToStart)) return true;
            if (GetCellType(cell.X - 1, cell.Y) == CellType.Wall && GetCellType(cell.X - 2, cell.Y) == CellType.Lane && !ConnectedAreas.Contains(Cells[cell.Y * Width + cell.X - 2].ToStart)) return true;
            if (GetCellType(cell.X, cell.Y + 1) == CellType.Lane && !ConnectedAreas.Contains(Cells[(cell.Y + 1) * Width + cell.X].ToStart)) return true;
            if (GetCellType(cell.X, cell.Y + 1) == CellType.Wall && GetCellType(cell.X, cell.Y + 2) == CellType.Lane && !ConnectedAreas.Contains(Cells[(cell.Y + 2) * Width + cell.X].ToStart)) return true;
            if (GetCellType(cell.X, cell.Y - 1) == CellType.Lane && !ConnectedAreas.Contains(Cells[(cell.Y - 1) * Width + cell.X].ToStart)) return true;
            if (GetCellType(cell.X, cell.Y - 1) == CellType.Wall && GetCellType(cell.X, cell.Y - 2) == CellType.Lane && !ConnectedAreas.Contains(Cells[(cell.Y - 2) * Width + cell.X].ToStart)) return true;
            return false;
        }

        private void Expand(Cell cell, Direction dir)
        {

            switch (dir)
            {
                case Direction.East:
                    {
                        uint nx = cell.X + 1;
                        uint ny = cell.Y;
                        uint nnx = cell.X + 2;
                        uint nny = cell.Y;
                        MergeArea(cell, nx, ny, nnx, nny);
                    }
                    break;
                case Direction.West:
                    {
                        uint nx = cell.X - 1;
                        uint ny = cell.Y;
                        uint nnx = cell.X - 2;
                        uint nny = cell.Y;
                        MergeArea(cell, nx, ny, nnx, nny);
                    }
                    break;
                case Direction.North:
                    {
                        uint nx = cell.X;
                        uint ny = cell.Y - 1;
                        uint nnx = cell.X;
                        uint nny = cell.Y - 2;
                        MergeArea(cell, nx, ny, nnx, nny);
                    }
                    break;
                case Direction.South:
                    {
                        uint nx = cell.X;
                        uint ny = cell.Y + 1;
                        uint nnx = cell.X;
                        uint nny = cell.Y + 2;
                        MergeArea(cell, nx, ny, nnx, nny);
                    }
                    break;
            }

        }

        private void MergeArea(Cell cell, uint nx, uint ny, uint nnx, uint nny)
        {
            if (GetCellType(nx, ny) == CellType.Wall)
            {
                if (GetCellType(nnx, nny) == CellType.Lane)
                {
                    uint fromarea = Cells[cell.Y * Width + cell.X].ToStart;
                    uint toarea = Cells[nny * Width + nnx].ToStart;
                    //if (fromarea != toarea)
                    if(!ConnectedAreas.Contains(toarea))
                    {
                        Cells[ny * Width + nx].Type = CellType.Lane;
                        Cells[ny * Width + nx].ToStart = fromarea;
                        ConnectedAreas.Add(toarea);
                        //if (fromarea > toarea)
                        //{
                        //    Cells.Where(c => c.Type == CellType.Lane && c.ToStart == fromarea).ToList().ForEach(nc => Cells[nc.Y * Width + nc.X].ToStart = toarea);
                        //}
                        //else
                        //{
                        //    Cells.Where(c => c.Type == CellType.Lane && c.ToStart == toarea).ToList().ForEach(nc => Cells[nc.Y * Width + nc.X].ToStart = fromarea);
                        //}
                    }
                }
            }
            else
            {
                uint fromarea = Cells[cell.Y * Width + cell.X].ToStart;
                uint toarea = Cells[ny * Width + nx].ToStart;
                if (!ConnectedAreas.Contains(toarea))
                { 
                    ConnectedAreas.Add(toarea); 
                }
                //if (fromarea != toarea)
                //{
                //    if (fromarea > toarea)
                //    {
                //        Cells.Where(c => c.Type == CellType.Lane && c.ToStart == fromarea).ToList().ForEach(nc => Cells[nc.Y * Width + nc.X].ToStart = toarea);
                //    }
                //    else
                //    {
                //        Cells.Where(c => c.Type == CellType.Lane && c.ToStart == toarea).ToList().ForEach(nc => Cells[nc.Y * Width + nc.X].ToStart = fromarea);
                //    }
                //}
            }
        }
    }
}
