using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Server.Communications.Config
{
    public static class GroupNames
    {
        public static string BoardGroup(int boardId)
        {
            return string.Format("Board:{0}", boardId);
        }

        public static string ChatRoomGroup(int chatRoomId)
        {
            return string.Format("ChatRoom:{0}", chatRoomId);
        }
    }
}
