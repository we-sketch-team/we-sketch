using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;
using WeSketch.Common;

namespace WeSketch.App.Controller
{
    public class WorkspaceController : IWorkspaceController
    {
        private IWorkspace workspace;
        private IWorkspaceView view;

        public void AddShape(IShape shape)
        {
            workspace.AddShape(shape);
        }

        public void Init(IWorkspace model, IWorkspaceView view)
        {
            this.workspace = model;
            this.view = view;
        }

        public void SendMessage(string sender, string text)
        {
            if (String.IsNullOrEmpty(text)) return;

            Message message = new Message()
            {
                Sender = sender,
                Text = text,
                BoardId = workspace.GetBoard().Id
            };

            workspace.SendMessage(message);
        }
    }
}
