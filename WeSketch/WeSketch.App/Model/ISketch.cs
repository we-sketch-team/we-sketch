using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Data;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Model
{
    public interface ISketch
    {
        User GetUser();
        void SetUser(User u);
        void SetBoard(Board board);
        Board GetBoard();
        void AddCollaborator(User u);
        void RemoveCollaborator(User u);
        void AddShape(IShape shape);
        void RemoveShape(IShape shape);
        void EditShape(IShape shape);
    }
}
