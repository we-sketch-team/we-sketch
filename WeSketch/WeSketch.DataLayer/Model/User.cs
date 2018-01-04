using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeSketch.DataLayer.Model
{
    public class User
    {
        public User()
        {
            ChatRooms = new HashSet<ChatRoom>();
            Messages = new HashSet<Message>();
            UserBoards = new HashSet<UserBoards>();
        }

        public int Id { get; set; }        
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool ActiveAccount { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual Board LockedBoard { get; set; }

        public virtual ICollection<ChatRoom> ChatRooms { get; set; }
        public ICollection<Message> Messages { get; set; }

        public virtual ICollection<UserBoards> UserBoards { get; set; }
    }
}
