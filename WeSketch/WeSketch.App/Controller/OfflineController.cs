using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public void Drag(Control control, double verticalOffset, double horizontalOffset)
        {
            throw new NotImplementedException();
        }

        public void DragCompleted()
        {
            throw new NotImplementedException();
        }

        public void Init(IWorkspace model, IWorkspaceView view)
		{
			this.workspace = model;
			this.view = view;
			view.SetController(this);
		}

        public void Resize(Control control, double verticalOfset, double horizontalOffset, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment)
        {
            throw new NotImplementedException();
        }

        public void ResizeCompleted()
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string sender, string text)
		{
			return;
		}
	}
}
