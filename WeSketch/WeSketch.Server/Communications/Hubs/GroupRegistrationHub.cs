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
        public static Dictionary<string, List<string>> BoardGroups = new Dictionary<string, List<string>>();

        private void AddUser(string groupName, string connectionId)
        {
            if (!BoardGroups.ContainsKey(groupName))
            {
                BoardGroups.Add(groupName, new List<string>());
            }
            var list = BoardGroups[groupName];
            if (!list.Contains(connectionId)) list.Add(connectionId);
        }

        private void RemoveUser(string groupName, string connectionId)
        {
            if (!BoardGroups.ContainsKey(groupName))
            {
                BoardGroups.Add(groupName, new List<string>());
            }
            var list = BoardGroups[groupName];
            list.Remove(connectionId);
        }

        public void RegisterToBoardGroup(int boardId)
        {
            string groupName = Config.GroupNames.BoardGroup(boardId);
            var message = $"User with connection id {Context.ConnectionId} subscribed to group {groupName}";
            Logger.Log(message);
            AddUser(groupName, Context.ConnectionId);
        }

        public Task RegisterToChatRoomGroup(int chatRoomId)
        {
            string groupName = Config.GroupNames.ChatRoomGroup(chatRoomId);
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public void UnsubscribeFromBoardGroup(int boardId)
        {
            string groupName = Config.GroupNames.BoardGroup(boardId);
            var message = $"User with connection id {Context.ConnectionId} unsubscribed from group {groupName}";
            Logger.Log(message);
            RemoveUser(groupName, Context.ConnectionId);
        }

        public Task UnsubscribeFromChatRoomGroup(int chatRoomId)
        {
            string groupName = Config.GroupNames.ChatRoomGroup(chatRoomId);
            return Groups.Remove(Context.ConnectionId, groupName);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var connectionId = Context.ConnectionId;

            foreach(string key in BoardGroups.Keys)
            {
                var group = BoardGroups[key];
                if(group.Contains(connectionId))
                {
                    RemoveUser(key, connectionId);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
