using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes
{
    public class ShapeEllipse:ShapeComponent
    {
        public ShapeEllipse(int width, int height, System.Windows.Media.Color color) : base()
        {
            MyElement = new System.Windows.Shapes.Ellipse
            {
                Width = width,
                Height = height,
                Fill = new System.Windows.Media.SolidColorBrush(color)

            };

            MyElement.Tag = this;
        }

        public ShapeEllipse()
        {
            MyElement = new System.Windows.Shapes.Ellipse();
        }
    }
}
