using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Model;
using WeSketch.App.View;

namespace WeSketch.App.Controller
{
    class SketchController : ISketchController
    {
        private ISketch myModel;
        private IView myView;
        private IAPI api;

        public SketchController()
        {
            api = new SketchService();
        }

        public bool AddCollaborator(string username)
        {
            User user = api.GetUserByUsername(username);
            if (user.Id == -1) return false;
            api.AddCollaborator(user, myModel.GetBoard()); // api add collab
            return true;
        }

        public bool RemoveCollaborator(User user)
        {
            api.RemoveCollaborator(user, myModel.GetBoard());
            return true;
        }

        public void AddShape(IShape shape)
        {
            myModel.AddShape(shape);
        }

        public void Init(ISketch model, IView view)
        {
            myModel = model;
            myView = view;
        }

        public void CreateBoard(string title, bool isPublic)
        {
            var user = myModel.GetUser();
            api.CreateBoard(title, isPublic, user);
        }

        public CollaboratorList GetCollaboratorList(Board board)
        {
            var list = api.GetBoardCollaborators(board);
            return list;
        }
    }
}
