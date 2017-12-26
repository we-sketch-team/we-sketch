using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.BusinessLogic.DTOs.ChatRoomDTOs
{
    public class UpdateChatRoomDTO
    {
        public int Id { get; set; }
        public bool ActiveChat { get; set; }
    }
}
