using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes
{
    public class Handle
    {
        private IShape parent;
        
        public Handle()
        {
            
        }

        public void SetParent(IShape shape)
        {
            parent = shape;
        }

        
    }
}
