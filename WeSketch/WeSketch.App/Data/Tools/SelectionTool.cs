using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;

namespace WeSketch.App.Data.Tools
{
    public class SelectionTool : ITool
    {
        private IShape selectedShape;
        private ISketch sketch;

        public void MouseDown(int x, int y)
        {
           
            throw new NotImplementedException();
        }

        public void MouseDrag(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void MouseUp(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
