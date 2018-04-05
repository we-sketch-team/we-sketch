﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data;
using WeSketch.App.Data.API;
using WeSketch.App.Data.Shapes;
using WeSketch.App.View;
using WeSketch.Common;

namespace WeSketch.App.Model
{
    public class Workspace : IWorkspace
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
            // TODO: rework method
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
            service.RemoveCollaborator(Global.CurrentUser, board);
            service.SetWorkspace(null);
        }

        public void DeleteShape(IShape shape)
        {
            board?.DeleteShape(shape);
        }

        public void Dispose()
        {
            LeaveQueue();
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
            board.Content = Utilities.ExportShapes(board.Shapes);
            //
            var found = Global.CurrentUser.Boards.Find(b => b.Id == board.Id);
            if (found != null)
            {
                found.Content = board.Content;
            }
            //
            service.UpdateBoardContent(board);
        }

        public void SetBoard(Board board)
        {
            CloseBoard();
            var service = SketchService.GetService();
            this.board = service.GetBoardById(board.Id);
            this.board.Shapes = Utilities.ImportShapes(this.board.Content);
            this.board.UserQueue = service.GetQueue(board);
            service.SetWorkspace(this);
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
            service.SendMessage(message);
        }

        public void UpdateUserQueue(BoardQueue queue)
        {
            if (observers.Count == 0)
                return;
            board.UserQueue = queue;
            observers.ForEach(obs => obs.RefreshUserQueue());
            IWorkspaceController ctrl;

            if (!board.CanEdit)
                ctrl = new GuestWorkspaceController();
            else            
                ctrl = new WorkspaceController();            

            ctrl.Init(this, observers[0]);                
        }

        public void EnterQueue()
        {
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            service.EnterQueue(user, board);
        }

        public void LeaveQueue()
        {
            var user = Global.CurrentUser;
            var service = SketchService.GetService();
            service.LeaveQueue(user, board);
        }
    }
}
