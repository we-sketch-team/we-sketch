using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.Server.NotificationsSystem;
using WeSketch.Server.Queues;

namespace ConsoleApp1
{
    class Program
    {
        private static DataService dataService = new DataService();

        static List<UserDetailsDTO> GetBoardCollaborators(int id)
        {
            return dataService.GetAllBoardCollaboratros(id);
        }

        static List<BoardDetailsDTO> GetUserBoards(int id)
        {
            return dataService.GetAllUserBoards(id);
        }

        static void CreateBoard()
        {
            CreateBoardDto createBoardDto = new CreateBoardDto()
            {
                UserId = 4,
                Desription = "again...",
                Title = "Title",
                PublicBoard = false
            };
            dataService.CreateBoard(createBoardDto);
        }

        static void SetBoardPreference()
        {
            BoardPreferenceDTO boardPreferenceDTO = new BoardPreferenceDTO
            {
                UserId = 4,
                BoardId = 74,
                IsFavorite = false
            };
            dataService.SetBoardPreference(boardPreferenceDTO);
        }

        static void AddCollaborator()
        {
            CollaboratorDTO collaboratorDTO = new CollaboratorDTO()
            {
                UserId = 2,
                BoardId = 74
            };

            dataService.AddCollaborator(collaboratorDTO);
        }

        static void RemoveCollaborator()
        {
            CollaboratorDTO collaboratorDTO = new CollaboratorDTO()
            {
                UserId = 2,
                BoardId = 54
            };

            dataService.RemoveCollaboratro(collaboratorDTO);
        }

        static void UpdateBoardContent()
        {
            BoardDetailsDTO boardDetailsDTO = new BoardDetailsDTO()
            {
                Content = "Updated from console app",
                Id = 4
            };
            dataService.UpdateBoardContent(boardDetailsDTO);
        }

		public static List<BoardDetailsDTO> GetMyBoards()
		{
			return dataService.GetMyBoardsBasicInformation(17);
		}

        static void Main(string[] args)
        {
			#region Boards update queues
			BoardUpdater board = new BoardUpdater
			{
				BoardId = 1,
				ConnectionId = "2"
			};

			BoardUpdater board1 = new BoardUpdater
			{
				BoardId = 1,
				ConnectionId = "21"
			};

			BoardUpdater boardw1 = new BoardUpdater
			{
				BoardId = 1,
				ConnectionId = "21w"
			};

			BoardUpdater board2 = new BoardUpdater
			{
				BoardId = 46,
				ConnectionId = "2"
			};

			BoardUpdater board3 = new BoardUpdater
			{
				BoardId = 32,
				ConnectionId = "2"
			};

			BoardUpdater board4 = new BoardUpdater
			{
				BoardId = 46,
				ConnectionId = "23"
			};

			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board1);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board2);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board3);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board4);

			foreach (var item in BoardsUpdateQueue.GetBoardQueue(1))
			{
				Console.WriteLine(item.ConnectionId);
			}		

			foreach (var item in BoardsUpdateQueue.GetBoardQueue(46))
			{
				Console.WriteLine(item.ConnectionId);
			}

			Console.WriteLine("remove");
			BoardsUpdateQueue.RemoveDisconnected("2");

			foreach (var item in BoardsUpdateQueue.GetBoardQueue(1))
			{
				Console.WriteLine(item.ConnectionId);
			}

			foreach (var item in BoardsUpdateQueue.GetBoardQueue(46))
			{
				Console.WriteLine(item.ConnectionId);
			}
			#endregion
		}
	}
}
