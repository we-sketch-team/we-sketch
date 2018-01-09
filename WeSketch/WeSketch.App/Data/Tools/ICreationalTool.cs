using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Data.Tools
{
    interface ICreationalTool: ITool
    {
        void SetController(ISketchController controller);
    }
}
