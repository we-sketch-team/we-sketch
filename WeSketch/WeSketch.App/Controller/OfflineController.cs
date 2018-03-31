using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
	public class OfflineWorkspaceController : IWorkspaceController
	{
		private IWorkspace workspace;
		private IWorkspaceView view;

		public void AddShape(IShape shape)
		{
			workspace.AddShape(shape);
		}

		public void DeleteShape(IShape selectedShape)
		{
			workspace.DeleteShape(selectedShape);
		}

		public void Init(IWorkspace model, IWorkspaceView view)
		{
			this.workspace = model;
			this.view = view;
			view.SetController(this);
		}

		public void SendMessage(string sender, string text)
		{
			return;
		}
	}
}
