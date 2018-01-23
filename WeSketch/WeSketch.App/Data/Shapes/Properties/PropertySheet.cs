using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes.Properties
{
    public class PropertySheet
    {
        public List<Property> Properties { get; set; }

        public PropertySheet()
        {
            Properties = new List<Property>();
        }

        public void AddProperty(Property prop)
        {
            Properties.Add(prop);
        }

        public void Refresh()
        {
            foreach(var prop in Properties)
            {
                prop.Apply();
            }
        }
    }
}
