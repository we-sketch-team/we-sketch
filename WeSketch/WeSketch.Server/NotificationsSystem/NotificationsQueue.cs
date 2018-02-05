using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.Server.NotificationsSystem
{
	public static class NotificationsQueue
	{
		private static Dictionary<int, Queue<Notification>> usersNotificationsQueues = new Dictionary<int, Queue<Notification>>();

		public static void AddToQueue(Notification notification)
		{
			int userId = notification.UserId;
			AddQueue(userId);
			usersNotificationsQueues[userId].Enqueue(notification);
		}

		public static Notification RemoveFromQueue(int userId)
		{
			if (usersNotificationsQueues[userId].Count == 0)
				return null;

			return usersNotificationsQueues[userId].Dequeue();
		}

		private static void AddQueue(int userId)
		{
			if (!usersNotificationsQueues.ContainsKey(userId))
				usersNotificationsQueues.Add(userId, new Queue<Notification>());
		}
	}
}
