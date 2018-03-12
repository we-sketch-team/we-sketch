using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Data.Shapes;
using WeSketch.App.View;
using WeSketch.Common;

namespace WeSketch.App.Model
{
    public class Workspace : IWorkspace, IBoardContentObserver
    {
        private Board board;
        private List<IWorkspaceView> observers;

        public Workspace()
        {
            observers = new List<IWorkspaceView>();
        }

        public bool AddCollaborator(string username)
        {
            var service = SketchService.GetService();
            var user = service.GetUserByUsername(username);
            if (user.Username != username) return false;

            service.AddCollaborator(user, board);
            return true;
        }

        public void AddShape(IShape shape)
        {
            board.AddShape(shape);
        }

        public void Attach(IWorkspaceView observer)
        {
            observers.Add(observer);
        }

        public void Detach(IWorkspaceView observer)
        {
            observers.Remove(observer);
        }

        public void CloseBoard()
        {
            if (board == null) return;

            var service = SketchService.GetService();
            service.UnsubscribeFromBoard(board);
            service.SetBoardContentObserver(null);
            board = null; // ?
        }

        public void DeleteShape(IShape shape)
        {
            board?.DeleteShape(shape);
        }

        public void Dispose()
        {
            CloseBoard();
        }

        public Board GetBoard()
        {
            return this.board;
        }

        public List<User> LoadBoardCollaborators()
        {
            var service = SketchService.GetService();
            var collaborators = service.GetBoardCollaborators(board);
            return collaborators;
        }

        public void MoveShape(IShape shape, Point newPosition)
        {
            shape.Move(newPosition.X, newPosition.Y);
        }

        public void RemoveCollaborator(User user)
        {
            var service = SketchService.GetService();
            service.RemoveCollaborator(user, board);
        }

        public void SaveBoard()
        {
            var service = SketchService.GetService();
            service.UpdateBoardContent(board);
        }

        public void SetBoard(Board board)
        {
            CloseBoard();
            var service = SketchService.GetService();
            this.board = service.GetBoardById(board.Id);
            this.board.Shapes = Utilities.ImportShapes(board.Content);
            service.SetBoardContentObserver(this);
            service.SubscribeToBoard(board);
        }

        public void UpdateBoardContent(Board updatedBoard)
        {
            if (board.Content == updatedBoard.Content) return;

            board.Content = updatedBoard.Content;
            board.Shapes = Utilities.ImportShapes(board.Content);
            board.Redraw();
        }

        public void UpdateMessage(Message message)
        {
            foreach(var obs in observers)
            {
                obs.UpdateMessage(message);
            }
        }

        public void SendMessage(Message message)
        {
            var service = SketchService.GetService();
            UpdateMessage(message);
            //service.SendMessage(message);
        }
    }
}
