using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.Common;

namespace WeSketch.App.View
{
    public interface IWorkspaceView
    {
        void Init(IWorkspace model);
        void MakeController();
        void RefreshCollaborators();
        void RefreshCanvas();
        void UpdateMessage(Message message);
        void RefreshUserQueue();
        void SetController(IWorkspaceController workspaceController);
        void Show();
        bool? ShowDialog();
    }
}
