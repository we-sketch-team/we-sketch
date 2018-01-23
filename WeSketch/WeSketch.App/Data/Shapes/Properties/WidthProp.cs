using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes.Properties
{
    public class WidthProp:Property
    {
        public override void Apply()
        {
            double val = Double.Parse(Value);
            Shape.SetWidth(val);
        }

        public override string Read()
        {
            string val = Shape.GetWidth().ToString();
            return val;
        }
    }
}
