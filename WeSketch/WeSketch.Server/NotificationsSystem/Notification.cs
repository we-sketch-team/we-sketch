using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Server.NotificationsSystem
{
	public class Notification
	{
		public enum NotificationType
		{
			AddedToBoard = 0,
			RemovedFromBoard
		}

		public string Content { get; set; }
		public int UserId { get; set; }
		public NotificationType Type { get; set; }
	}
}
