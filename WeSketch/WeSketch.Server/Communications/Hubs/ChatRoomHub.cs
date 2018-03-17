using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.DTOs.ChatRoomDTOs;
using WeSketch.BusinessLogic.Services;
using WeSketch.Common;

namespace WeSketch.Server.Communications.Hubs
{
    public class ChatRoomHub : Hub
    {
		public static Dictionary<string, List<string>> ChatGroups = new Dictionary<string, List<string>>();

		private void AddUser(string groupName, string connectionId)
		{
			if (!ChatGroups.ContainsKey(groupName))
			{
				ChatGroups.Add(groupName, new List<string>());
			}
			var list = ChatGroups[groupName];
			if (!list.Contains(connectionId)) list.Add(connectionId);
		}

		private void RemoveUser(string groupName, string connectionId)
		{
			if (!ChatGroups.ContainsKey(groupName))
			{
				ChatGroups.Add(groupName, new List<string>());
			}
			var list = ChatGroups[groupName];
			list.Remove(connectionId);
		}

		public void RegisterToChatGroup(int chatId)
		{
			string groupName = Config.GroupNames.ChatRoomGroup(chatId);
			var message = $"User with connection id {Context.ConnectionId} subscribed to chat room {groupName}";
			Logger.Log(message);
			AddUser(groupName, Context.ConnectionId);
		}		

		public void UnsubscribeFromChatGroup(int chatId)
		{
			string groupName = Config.GroupNames.ChatRoomGroup(chatId);
			var message = $"User with connection id {Context.ConnectionId} unsubscribed from chat room {groupName}";
			Logger.Log(message);
			RemoveUser(groupName, Context.ConnectionId);
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			var connectionId = Context.ConnectionId;

			foreach (string key in ChatGroups.Keys)
			{
				var group = ChatGroups[key];
				if (group.Contains(connectionId))
				{
					RemoveUser(key, connectionId);
				}
			}

			return base.OnDisconnected(stopCalled);
		}

		public void SendMessage(Message message)
		{
			string groupName = Config.GroupNames.ChatRoomGroup(message.BoardId);
			var group = ChatGroups[groupName];
			group.ForEach(u => Clients.Client(u).ReceiveMessage(message));
		}
	}
}
