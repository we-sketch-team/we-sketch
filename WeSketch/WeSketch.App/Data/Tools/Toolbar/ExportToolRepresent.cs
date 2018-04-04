using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools.Toolbar
{
	public class ExportToolRepresent : ToolbarItemRepresent
	{
		public ExportToolRepresent(Toolbar toolbar, IDrawable drawable)
		{
			this.toolbar = toolbar;
			tool = new ExportTool(drawable);
			this.AttachTool(tool);
		}

		protected override void SetButtonIcon()
		{
			this.Content = "Export";
		}
	}
}
