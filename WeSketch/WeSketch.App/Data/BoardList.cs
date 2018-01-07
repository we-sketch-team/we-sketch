using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data
{
    public class BoardList
    {
        public List<Board> Boards { get; set; }

        public BoardList()
        {
            Boards = new List<Board>();
        }
    }
}
