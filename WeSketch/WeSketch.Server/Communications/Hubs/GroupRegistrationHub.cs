using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Server.Communications.Hubs
{
    public class GroupRegistrationHub : Hub
    {
        public Task RegisterToBoardGroup(int boardId)
        {
            string groupName = Config.GroupNames.BoardGroup(boardId);
            var message = $"User with connection id {Context.ConnectionId} subscribed to group {groupName}";
            Logger.Log(message);
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task RegisterToChatRoomGroup(int chatRoomId)
        {
            string groupName = Config.GroupNames.ChatRoomGroup(chatRoomId);
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task UnsubscribeFromBoardGroup(int boardId)
        {
            string groupName = Config.GroupNames.BoardGroup(boardId);
            var message = $"User with connection id {Context.ConnectionId} unsubscribed from group {groupName}";
            Logger.Log(message);
            return Groups.Remove(Context.ConnectionId, groupName);
        }

        public Task UnsubscribeFromChatRoomGroup(int chatRoomId)
        {
            string groupName = Config.GroupNames.ChatRoomGroup(chatRoomId);
            return Groups.Remove(Context.ConnectionId, groupName);
        }
    }
}
