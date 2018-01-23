using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Model
{
    public class Workspace : IWorkspace
    {
        private Board board;
        private IAPI api;

        public Workspace()
        {
            api = new SketchService();
        }

        public bool AddCollaborator(string username)
        {
            var user = api.GetUserByUsername(username);
            if (user.Username != username) return false;

            api.AddCollaborator(user, board);
            return true;
        }

        public void AddShape(IShape shape)
        {
            board.AddShape(shape);
        }

        public Board GetBoard()
        {
            return this.board;
        }

        public CollaboratorList LoadBoardCollaborators()
        {
            var collaborators = api.GetBoardCollaborators(board);
            return collaborators;
        }

        public void RemoveCollaborator(User user)
        {
            api.RemoveCollaborator(user, board);
        }

        public void SaveBoard()
        {
            api.UpdateBoardContent(board);
        }

        public void SetBoard(Board board)
        {
            this.board = board;
        }
    }
}
