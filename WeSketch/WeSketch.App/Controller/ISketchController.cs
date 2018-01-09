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
    public interface ISketchController
    {
        void Init(ISketch model, IView view);
        bool AddCollaborator(string username);
        bool RemoveCollaborator(User user);
        void CreateBoard(string title, bool isPublic);
        void AddShape(IShape shape);
        CollaboratorList GetCollaboratorList(Board board);
    }
}
