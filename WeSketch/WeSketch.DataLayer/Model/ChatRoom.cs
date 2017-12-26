using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.DataLayer.Model
{
    public class ChatRoom
    {
        public ChatRoom()
        {
            UsersInChatRoom = new HashSet<User>();
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public bool ActiveChat { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Board Board { get; set; }

        public virtual ICollection<User> UsersInChatRoom { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
