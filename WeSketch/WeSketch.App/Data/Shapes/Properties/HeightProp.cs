using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes.Properties
{
    public class HeightProp:Property
    {
        public override void Apply()
        {
            double val = Double.Parse(Value);
            Shape.SetHeight(val);
        }

        public override string Read()
        {
            string val = Shape.GetHeight().ToString();
            return val;
        }
    }
}
