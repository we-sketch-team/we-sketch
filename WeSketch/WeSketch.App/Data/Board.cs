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
        public string Content { get;set; }
        public ShapeList Shapes { get; set; }
        public CollaboratorList Collaborators { get; set; }
        public Canvas MyCanvas { get; set; }


        public Board()
        {
            Shapes = new ShapeList();
            Collaborators = new CollaboratorList();
        }

        public void AddShape(IShape shape)
        {
            Shapes.Add(shape);
            shape.Draw(MyCanvas);
        }

        public void DeleteShape(IShape shape)
        {
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
