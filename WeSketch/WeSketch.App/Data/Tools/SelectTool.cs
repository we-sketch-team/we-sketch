using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools
{
    public class SelectTool : ITool
    {
        private IDrawable form;

        public SelectTool(IDrawable form)
        {
            this.form = form;
        }

        public void Activate()
        {
            //throw new NotImplementedException();
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
            var elem = form.GetCanvas().InputHitTest(new System.Windows.Point(x, y));
            Shape winShape = elem as Shape;
            if (winShape.Tag is IShape shape)
                form.SelectShape(shape);
        }
    }
}
