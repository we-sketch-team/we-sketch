using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;

namespace WeSketch.App.Data.Tools.Toolbar
{
    public class EllipseToolRepresent:ToolbarItemRepresent
    {

        public EllipseToolRepresent(Toolbar toolbar)
        {
            this.toolbar = toolbar;
            tool = new EllipseCreationalTool();
            tool.SetController(toolbar.Controller);
            this.AttachTool(tool);
        }

        protected override void SetButtonIcon()
        {
            this.Content = "Elli";
        }
    }
}
