using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WeSketch.App.Data.Shapes;

namespace WeSketch.App.Forms
{
    public interface IDrawable
    {
        Canvas GetCanvas();
        void SelectShape(IShape shape);
    }
}
