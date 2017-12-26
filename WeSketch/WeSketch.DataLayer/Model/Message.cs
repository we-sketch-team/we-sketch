using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.DataLayer.Model
{
    public class Message
    {
        public Message()
        {

        }

        public int Id { get; set; }
        public string Content { get; set; }

        public int UserID { get; set; }
        public int ChatRoomId { get; set; }

        public virtual ChatRoom SentIn { get; set; }
        public virtual User Sender { get; set; }
    }
}
