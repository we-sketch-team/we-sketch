using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using WeSketch.App.Controller;
using WeSketch.App.Data.Shapes;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools
{
    public class SelectTool : ITool
    {
        private IDrawable form;
        private IWorkspaceController controller;

        public SelectTool(IDrawable form)
        {
            this.form = form;
        }

        public void Activate()
        {
            
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
            var clicked = form.GetCanvas().InputHitTest(new System.Windows.Point(x, y));
            if (clicked == null)
                return;
            var elem = clicked as Rectangle;
            if (elem == null)
                return;
            var contentControl = elem.DataContext as ContentControl;
            if (contentControl == null)
                return;
            var frameworkShape = contentControl.Content as Shape;
            if (frameworkShape == null)
                return;
            var shape = frameworkShape.Tag as IShape;
            if (shape == null)
                return;

            form.SelectShape(shape);
        }

        public void SetController(IWorkspaceController controller)
        {
            this.controller = controller;
        }
    }
}
