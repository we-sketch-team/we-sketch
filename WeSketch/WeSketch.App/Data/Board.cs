using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Data
{
    [Serializable]
    public class Board
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ShapeList Shapes { get; set; }
        public CollaboratorList Collaborators { get; set; }
        public Canvas MyCanvas { get; set; }
        public string Role { get; set; }
		public bool IsPublic { get; set; }
        public BoardQueue UserQueue { get; set; }
        public bool IsPasswordProtected { get; set; }

        public bool CanEdit
        {
            get
            {
                if (UserQueue.IsEmpty()) return false;
                return UserQueue.GetFirst().Id == Global.CurrentUser.Id;
            }
        }


        public Board()
        {
            Shapes = new ShapeList();
            Collaborators = new CollaboratorList();
            UserQueue = new BoardQueue();
        }

        public void AddShape(IShape shape)
        {
            Shapes.Add(shape);
            shape.Draw(MyCanvas);
        }

        public void DeleteShape(IShape shape)
        {
            if (shape == null) return;
            Shapes.Remove(shape);
            shape.Delete(MyCanvas);
        }

        public void Draw(Canvas canvas)
        {
            canvas.Children.Clear();

            foreach(var shape in Shapes)
            {
                shape.Draw(canvas);
            }
        }

        public void Redraw()
        {
            Draw(MyCanvas);
        }
    }
}
