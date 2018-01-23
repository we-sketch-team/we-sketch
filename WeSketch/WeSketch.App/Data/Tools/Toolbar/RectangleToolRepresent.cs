using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;

namespace WeSketch.App.Data.Tools.Toolbar
{
    public class RectangleToolRepresent:ToolbarItemRepresent
    {

        public RectangleToolRepresent(IWorkspaceController controller)
        {
            ICreationalTool tool = new RectangleCreationalTool();
            tool.SetController(controller);
            this.AttachTool(tool);
        }

        protected override void SetButtonIcon()
        {
            this.Content = "Rect";
        }
    }
}
