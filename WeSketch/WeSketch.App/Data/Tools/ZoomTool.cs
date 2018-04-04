using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WeSketch.App.Controller;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools
{
    public class ZoomTool : ITool
    {
        protected IDrawable workspace;
        protected double modifier = 0.1;
        protected static double Scale = 1.0;

        public ZoomTool(IDrawable workspace)
        {
            this.workspace = workspace;
        }

        public void Activate()
        {
            var canvas = workspace.GetCanvas();
            Scale = CalculateScale(Scale);
            ScaleTransform scaleT = new ScaleTransform(Scale, Scale);
            canvas.RenderTransform = scaleT;
        }

        protected virtual double CalculateScale(double scale)
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            //throw new NotImplementedException();
        }

        public void MouseDown(int x, int y)
        {
            //throw new NotImplementedException();
        }

        public void MouseUp(int x, int y)
        {
            //throw new NotImplementedException();
        }

        public void SetController(IWorkspaceController controller)
        {
            //
        }
    }
}
