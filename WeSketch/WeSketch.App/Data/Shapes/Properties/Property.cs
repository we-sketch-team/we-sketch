using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data.Shapes.Properties
{
    public abstract class Property
    {
        private IShape shape;
        private string name;

        protected IShape Shape { get => shape; set => shape = value; }
        protected string Name { get => name; set => name = value; }
        protected string Value { get => Read(); set => Apply(); }

        public virtual void Apply()
        {
            throw new NotImplementedException();
        }

        public virtual string Read()
        {
            throw new NotImplementedException();
        }
    }
}
