using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools.Toolbar
{
    public class ZoomInToolRepresent: ToolbarItemRepresent
    {
        public ZoomInToolRepresent(IDrawable form)
        {
            tool = new ZoomInTool(form);
            this.AttachTool(tool);
        }

        protected override void SetButtonIcon()
        {
            this.Content = "Z+";
        }
    }
}
