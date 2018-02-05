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
		private static Dictionary<int, Queue<BoardUpdater>> boardsUpdateQueues = new Dictionary<int, Queue<BoardUpdater>>();		

		public static void AddToQueue(BoardUpdater updater)
		{
			int boardId = updater.BoardId;
			AddQueue(boardId);
			boardsUpdateQueues[boardId].Enqueue(updater);
		}

		private static void AddQueue(int boardId)
		{
			if (!boardsUpdateQueues.ContainsKey(boardId))
				boardsUpdateQueues.Add(boardId, new Queue<BoardUpdater>());
		}

		public static BoardUpdater RemoveFromQueue(int boardId)
		{
			if (boardsUpdateQueues[boardId].Count == 0)
				return null;

			return boardsUpdateQueues[boardId].Dequeue();
		}

		public static Queue<BoardUpdater> GetBoardQueue(int boardId)
		{
			if (boardsUpdateQueues[boardId].Count == 0)
				return null;

			return boardsUpdateQueues[boardId];
		}
	}
}
