using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data
{
    public class CollaboratorList
    {

        public List<User> Users { get; set; }

        public CollaboratorList()
        {
            Users = new List<User>();
        }


        public void Add(User u)
        {
            Users.Add(u);
        }

        public void Remove(User u)
        {
            Users.Remove(u);
        }

        public override string ToString()
        {
            if (this.Users.Count == 0) return "";

            string colls = "";
            this.Users.ForEach(u => colls += u.Username + ", ");

            colls = colls.Substring(0, colls.Length - 2);
            return colls;
        }
    }
}
