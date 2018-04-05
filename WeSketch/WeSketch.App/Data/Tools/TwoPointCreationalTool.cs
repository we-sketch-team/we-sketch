using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Controller;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Data.Tools
{
    public abstract class TwoPointCreationalTool : ICreationalTool
    {
        protected Point startPoint;
        protected Point endPoint;
        protected IWorkspaceController controller;

        public void Activate()
        {
            //throw new NotImplementedException();
        }

        public void Deactivate()
        {
            //throw new NotImplementedException();
        }

        public virtual IShape GetShapeInstance()
        {
            throw new NotImplementedException();
        }

        public void MouseDown(int x, int y)
        {
            startPoint = new Point(x, y);
        }

        public void MouseUp(int x, int y)
        {
            endPoint = new Point(x, y);
            var shape = GetShapeInstance();
            controller.AddShape(shape);
        }

        public void SetController(IWorkspaceController controller)
        {
            this.controller = controller;
        }
    }
}
