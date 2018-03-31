using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;
using WeSketch.Common;

namespace WeSketch.App.Controller
{
    public class GuestWorkspaceController : IWorkspaceController
    {
        private IWorkspace workspace;
        private IWorkspaceView view;

        public void AddShape(IShape shape)
        {
            
        }

        public void DeleteShape(IShape selectedShape)
        {
            
        }

        public void Drag(Control control, double verticalOffset, double horizontalOffset)
        {

        }

        public void Init(IWorkspace model, IWorkspaceView view)
        {
            this.workspace = model;
            this.view = view;
            view.SetController(this);
        }

        public void Resize(Control control, double verticalOfset, double horizontalOffset, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment)
        {
        }

        public void SendMessage(string sender, string text)
        {
            
        }
    }
}
