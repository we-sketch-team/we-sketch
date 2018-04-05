using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes
{
    public static class ShapesFactory
    {
        public static IShape GetRectangleInstance()
        {
            return new ShapeRectangle();
        }

        public static IShape GetEllipseInstance()
        {
            return new ShapeEllipse();
        }
    }
}
