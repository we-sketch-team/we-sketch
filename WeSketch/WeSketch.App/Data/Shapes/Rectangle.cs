using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WeSketch.App.Data.Shapes
{
    class Rectangle: Shape
    {
        public Rectangle(int width, int height, System.Windows.Media.Color color) : base()
        {
            myElement = new System.Windows.Shapes.Rectangle
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(color)
            };
        }

        public Rectangle()
        {
            myElement = new System.Windows.Shapes.Rectangle();
        }
    }
}
