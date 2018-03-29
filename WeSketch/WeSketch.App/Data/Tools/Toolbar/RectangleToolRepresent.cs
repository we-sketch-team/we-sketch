using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;

namespace WeSketch.App.Data.Tools.Toolbar
{
    public class RectangleToolRepresent: ToolbarItemRepresent
    {
        public RectangleToolRepresent(Toolbar toolbar)
        {
            this.toolbar = toolbar;
            tool = new RectangleCreationalTool();
            tool.SetController(toolbar.Controller);
            this.AttachTool(tool);

        }

        protected override void SetButtonIcon()
        {
            this.Content = "Rect";
        }
    }
}
