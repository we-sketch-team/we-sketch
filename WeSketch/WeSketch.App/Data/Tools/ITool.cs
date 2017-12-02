using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Tools
{
    public interface ITool
    {
        void MouseDown(int x, int y);
        void MouseUp(int x, int y);
        void MouseDrag(int x, int y);
    }
}
