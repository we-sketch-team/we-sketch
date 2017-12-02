using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Data
{
    public class Board
    {
        public string Title { get; set; }
        public List<User> Collaborators { get; set; }
        public List<IShape> Content { get; set; }
        public Canvas MyCanvas { get; set; }

        public Board()
        {
            Content = new List<IShape>();
        }

        public void AddShape(IShape shape)
        {
            Content.Add(shape);
            shape.Draw(MyCanvas);
        }

        public void RemoveShape(IShape shape)
        {
            Content.Remove(shape);
        }

        public void EditShape(IShape shape)
        {

        }

        public void Draw(Canvas canvas)
        {
            canvas.Children.Clear();

            foreach(var shape in Content)
            {
                shape.Draw(canvas);
            }
        }
    }
}
