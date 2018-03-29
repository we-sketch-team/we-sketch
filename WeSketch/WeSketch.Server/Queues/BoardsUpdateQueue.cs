using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;

namespace WeSketch.Server.Queues
{
	public static class BoardsUpdateQueue
	{
		private static Dictionary<int, List<BoardUpdater>> boardsUpdateQueues = new Dictionary<int, List<BoardUpdater>>();		

		public static void AddToQueue(BoardUpdater updater)
		{
			int boardId = updater.BoardId;
			AddQueue(boardId);
			boardsUpdateQueues[boardId].Add(updater);
		}

		private static void AddQueue(int boardId)
		{
			if (!boardsUpdateQueues.ContainsKey(boardId))
				boardsUpdateQueues.Add(boardId, new List<BoardUpdater>());
		}

		public static void RemoveFromQueue(int boardId, string connectionId)
		{
			if (boardsUpdateQueues[boardId].Count == 0)
				return;

			BoardUpdater boardUpdater = boardsUpdateQueues[boardId].Find(x => x.ConnectionId == connectionId);
			boardsUpdateQueues[boardId].Remove(boardUpdater);
		}

		public static List<BoardUpdater> GetBoardQueue(int boardId)
		{
            if (!boardsUpdateQueues.ContainsKey(boardId))            
                AddQueue(boardId);            

			return boardsUpdateQueues[boardId];
		}

		public static List<int> RemoveDisconnected(string connectionId)
		{
			List<int> boardsLeft = new List<int>();

			foreach (var dictionaryItem in boardsUpdateQueues)
			{
				List<BoardUpdater> queue = dictionaryItem.Value;
				BoardUpdater boardUpdater = queue.Find(x => x.ConnectionId == connectionId);

				if (boardUpdater == null)
					continue;

				queue.Remove(boardUpdater);
				boardsLeft.Add(boardUpdater.BoardId);
			}

			return boardsLeft;
		}
	}
}
