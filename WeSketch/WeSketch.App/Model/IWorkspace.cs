using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Model
{
    public interface IWorkspace : IDisposable
    {
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
    }
}
