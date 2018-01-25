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
                Fill = new System.Windows.Media.SolidColorBrush(color),
                Tag = this
            };
        }

        public ShapeEllipse()
        {
            MyElement = new System.Windows.Shapes.Ellipse()
            {
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0)),
                Tag = this
            };
        }
    }
}
