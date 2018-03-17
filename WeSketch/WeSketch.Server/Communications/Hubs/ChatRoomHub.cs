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

		public void SendMessage(Message message)
		{
			string groupName = Config.GroupNames.BoardGroup(message.BoardId);
            var group = GroupRegistrationHub.BoardGroups[groupName];
            Logger.Log($"User:{message.Sender} sent message to board:{message.BoardId}");
            group.ForEach(u => Clients.Client(u).ReceiveMessage(message));
            //Clients.All.ReceiveMessage(message);
		}
	}
}
