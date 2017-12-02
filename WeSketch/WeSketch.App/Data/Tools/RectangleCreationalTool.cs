using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data.Shapes;
using WeSketch.App.View;

namespace WeSketch.App.Data.Tools
{
    public class RectangleCreationalTool : ICreationalTool
    {
        private Point startPoint;
        private Point endPoint;
        private ISketchController controller;

        public RectangleCreationalTool()
        {

        }

        public IShape GetShape()
        {
            int width = Math.Abs(startPoint.X - endPoint.X);
            int height = Math.Abs(startPoint.Y - endPoint.Y);

            int setPosX = Math.Min(startPoint.X, endPoint.X);
            int setPosY = Math.Min(startPoint.Y, endPoint.Y);
            Shapes.Rectangle rect = new Shapes.Rectangle(width, height, System.Windows.Media.Color.FromRgb(0, 0,0));
            rect.Move(setPosX, setPosY);
            return rect;
            
        }

        public void MouseDown(int x, int y)
        {
            startPoint = new Point(x, y);
        }

        public void MouseDrag(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void MouseUp(int x, int y)
        {
            endPoint = new Point(x, y);
            controller.AddShape(GetShape());
        }

        public void SetController(ISketchController controller)
        {
            this.controller = controller;
        }
    }
}
