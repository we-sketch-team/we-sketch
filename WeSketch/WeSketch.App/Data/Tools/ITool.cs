using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;

namespace WeSketch.App.Data.Tools
{
    public interface ITool
    {
        void Activate();
        void Deactivate();
        void MouseDown(int x, int y);
        void MouseUp(int x, int y);
        void SetController(IWorkspaceController controller);
    }
}
