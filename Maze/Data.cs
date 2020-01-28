using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal enum CellType : byte { Wall, Lane }
    internal enum Direction : byte { East, West,North,South }
    internal struct Cell
    {
        public uint X;
        public uint Y;
        public CellType Type;
        public uint ToStart;
    }
    internal struct Step {
        public uint idx;
        public Direction dir;
    }
}
