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
		private static Dictionary<int, Queue<BoardDetailsDTO>> boardsUpdateQueues = new Dictionary<int, Queue<BoardDetailsDTO>>();		

		public static void AddToQueue(BoardDetailsDTO board)
		{
			int boardId = board.Id;
			AddQueue(boardId);
			boardsUpdateQueues[boardId].Enqueue(board);
		}

		public static BoardDetailsDTO RemoveFromQueue(int boardId)
		{
			return boardsUpdateQueues[boardId].Dequeue();
		}

		private static void AddQueue(int boardId)
		{
			if (!boardsUpdateQueues.ContainsKey(boardId))
				boardsUpdateQueues.Add(boardId, new Queue<BoardDetailsDTO>());
		}
	}
}
