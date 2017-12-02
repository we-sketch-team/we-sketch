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
        private List<IObserver> observers;

        public Sketch()
        {
            observers = new List<IObserver>();
            //board = new Board();
        }

        public void AddCollaborator(User u)
        {
            board.Collaborators.Add(u);
        }

        public void AddShape(IShape shape)
        {
            board.AddShape(shape);
            NotifyObservers();
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void EditShape(IShape shape)
        {
            throw new NotImplementedException();
        }

        public void NotifyObservers()
        {
            observers.ForEach(obs => obs.InvokeUpdate());
        }

        public void OpenBoard(Board board)
        {
            this.board = board;
        }

        public void RemoveCollaborator(User u)
        {
            board.Collaborators.Remove(u);
        }

        public void RemoveShape(IShape shape)
        {
            board.RemoveShape(shape);
            NotifyObservers();
        }

        public void SetUser(User u)
        {
            user = u;
        }
    }
}
