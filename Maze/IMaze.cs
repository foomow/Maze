using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    interface IMaze
    {
        Action Fun_onExpand { get; set; }
        Action<uint, bool> Fun_onStep { get; set; }
        Action<uint, bool> Fun_onBack { get; set; }
        Cell[] Cells { get; set; }
        uint Height { get; set; }
        uint Width { get; set; }

        void Init(uint h, uint w, int seed = 1);
        void Build();
        uint[] GetPath(uint startIdx, uint endIdx);

    }
}
