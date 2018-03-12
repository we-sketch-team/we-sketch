using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;
using WeSketch.App.View;
using WeSketch.Common;

namespace WeSketch.App.Model
{
    public interface IWorkspace : IDisposable
    {
        void Attach(IWorkspaceView observer);
        void Detach(IWorkspaceView observer);
        void AddShape(IShape shape);
        void DeleteShape(IShape shape);
        void MoveShape(IShape shape, Point newPosition);
        bool AddCollaborator(string username);
        void RemoveCollaborator(User user);
        void SetBoard(Board board);
        void CloseBoard();
        Board GetBoard();
        void SaveBoard();
        List<User> LoadBoardCollaborators();
        void UpdateMessage(Message message);
        void SendMessage(Message message);
    }
}
