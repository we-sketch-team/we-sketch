using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    public interface IWorkspaceController
    {
        void Init(IWorkspace model, IWorkspaceView view);
        void AddShape(IShape shape);
        void AddCollaborator(string username);
        void RemoveCollaborator(User user);
    }
}
