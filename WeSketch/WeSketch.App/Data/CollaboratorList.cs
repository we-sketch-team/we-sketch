using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data
{
    public class CollaboratorList: List<User>
    {

        public override string ToString()
        {
            if (this.Count == 0) return "";

            string colls = "";
            foreach(var c in this)
            {
                colls += c.Username + ", ";
            }

            colls = colls.Substring(0, colls.Length - 2);
            return colls;
        }
    }
}
