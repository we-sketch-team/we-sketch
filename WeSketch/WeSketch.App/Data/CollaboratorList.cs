using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data
{
    public class CollaboratorList
    {

        public List<User> Collaborators { get; set; }

        public CollaboratorList()
        {
            Collaborators = new List<User>();
        }


        public void Add(User u)
        {
            Collaborators.Add(u);
        }

        public void Remove(User u)
        {
            Collaborators.Remove(u);
        }

        public override string ToString()
        {
            if (this.Collaborators.Count == 0) return "";

            string colls = "";
            this.Collaborators.ForEach(u => colls += u.Username + ", ");

            colls = colls.Substring(0, colls.Length - 2);
            return colls;
        }
    }
}
