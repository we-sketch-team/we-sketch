using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.BusinessLogic.Services;
using WeSketch.Common;
using WeSketch.Server.Queues;

namespace WeSketch.Server.Communications.Hubs
{
    public class BoardHub : Hub
    {
        private DataService dataService = BusinessLogic.Utilities.ObjectFactory.GetDataService();

        public List<BoardDetailsDTO> GetMyBoards(int userId)
        {
            var message = $"Board list requested from user with id: {userId}";
            Logger.Log(message);
            return dataService.GetMyBoardsBasicInformation(userId);
        }

        public CreateBoardDto CreateBoard(CreateBoardDto createBoardDto)
        {
            return dataService.CreateBoard(createBoardDto);
        }

        public BoardDetailsDTO GetBoardWithRole(int userId, int boardId)
        {
            var message = $"Board requested with id: {boardId}";
            Logger.Log(message);
            return dataService.GetBoardWithRole(userId, boardId);
        }

        public BoardDetailsDTO UpdateBoardContent(BoardDetailsDTO boardDetailsDTO)
        {
            BoardDetailsDTO board = dataService.UpdateBoardContent(boardDetailsDTO);
            var groupName = Config.GroupNames.BoardGroup(boardDetailsDTO.Id);
            Logger.Log($"Updated content for board with id: {board.Id}");
            Logger.Log($"Sent update notification to group with name: {groupName}");
            var group = GroupRegistrationHub.BoardGroups[groupName];
            group.ForEach(u => Clients.Client(u).NotifyBoardUpdate(board));
            return board;
        }

        public BoardDetailsDTO UpdateBoard(BoardDetailsDTO boardDetailsDTO)
        {
            BoardDetailsDTO board = dataService.UpdateBoard(boardDetailsDTO);
            Clients.Others.UpdateBoardInformation(boardDetailsDTO);
            return board;
        }

        public void DeleteBoard(int boardId)
        {
            dataService.DeleteBoard(boardId);
        }

        public List<UserDetailsDTO> GetCollaborators(int boardId)
        {
            return dataService.GetAllBoardCollaboratros(boardId);
        }

        public void AddCollaborator(CollaboratorDTO collaboratorDTO)
        {
            dataService.AddCollaborator(collaboratorDTO);
            Clients.Others.NotifyCollaboratorAddition(collaboratorDTO);
        }

		public List<BoardDetailsDTO> GetPublicBoards()
		{
			return dataService.GetAllPublicBoards();
		}

        public List<BoardDetailsDTO> GetSharedBoardsWithUser(int userId)
        {
            var data = dataService.GetBoardsSharedWithUser(userId);
            return data;
        }

		public void EnterQueue(BoardUpdater updater)
		{
			BoardsUpdateQueue.AddToQueue(updater);
			var groupName = Config.GroupNames.BoardGroup(updater.BoardId);
			var group = GroupRegistrationHub.BoardGroups[groupName];
			group.ForEach(u => Clients.Client(u).UserEnteredQueueNotify(updater.UserId));
		}

		public void LeaveQueue(BoardUpdater updater)
		{
			BoardsUpdateQueue.RemoveFromQueue(updater.BoardId, Context.ConnectionId);
			var groupName = Config.GroupNames.BoardGroup(updater.BoardId);
			var group = GroupRegistrationHub.BoardGroups[groupName];
			group.ForEach(u => Clients.Client(u).UserLeftQueueNotify(updater.UserId));
		}

		public List<BoardUpdater> GetBoardQueue(int boardId)
		{
			return BoardsUpdateQueue.GetBoardQueue(boardId);
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			List<int> boardsLeft = BoardsUpdateQueue.RemoveDisconnected(Context.ConnectionId);

			foreach (var id in boardsLeft)
			{
				var groupName = Config.GroupNames.BoardGroup(id);
				var group = GroupRegistrationHub.BoardGroups[groupName];
				group.ForEach(u => Clients.Client(u).UserLeftQueueNotify(id));
			}

			return base.OnDisconnected(stopCalled);
		}
	}
}
