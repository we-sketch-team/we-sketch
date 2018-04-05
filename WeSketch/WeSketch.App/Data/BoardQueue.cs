using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.App.Data
{
    public class BoardQueue
    {
        private List<User> usersInQueue;

        public BoardQueue()
        {
            usersInQueue = new List<User>();
        }

        public User GetFirst()
        {
            return usersInQueue.First();
        }

        public void Enqueue(User user)
        {
            usersInQueue.Add(user);
        }

        public void RemoveFromQueue(int userId)
        {
            var user = usersInQueue.Find(u => u.Id == userId);
            usersInQueue.Remove(user);
        }


        public bool Contains(User user)
        {
            return usersInQueue.Exists(u=>u.Id == user.Id);                
        }

        public override string ToString()
        {
            if (IsEmpty()) return "Queue";

            string res = "";
            foreach (var u in usersInQueue)
            {
                res += $"{u.Username} "; 
            }
            return res;
        }

        public bool IsEmpty()
        {
            return usersInQueue.Count == 0;
        }
    }
}
