using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Model
{
    class Sketch : ISketch
    {
        private User user;
        private Board board;

        public Sketch()
        {
            board = new Board();
        }

        public void AddCollaborator(User u)
        {
            board.Collaborators.Add(u);
        }

        public void AddShape(IShape shape)
        {
            board.AddShape(shape);
        }

        public void EditShape(IShape shape)
        {
            throw new NotImplementedException();
        }

        public Board GetBoard()
        {
            return board;
        }

        public User GetUser()
        {
            return user;
        }

        public void RemoveCollaborator(User u)
        {
            board.Collaborators.Remove(u);
        }

        public void RemoveShape(IShape shape)
        {
            board.RemoveShape(shape);
        }

        public void SetBoard(Board board)
        {
            this.board = board;
        }

        public void SetUser(User u)
        {
            user = u;
        }
    }
}
