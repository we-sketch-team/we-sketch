using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.App.Forms;

namespace WeSketch.App.Data.Tools
{
    public class ZoomOutTool : ZoomTool
    {

        public ZoomOutTool(IDrawable drawable) : base(drawable)
        {

        }

        protected override double CalculateScale(double scale)
        {
            return scale - modifier;
        }
    }
}
