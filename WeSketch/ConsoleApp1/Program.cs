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

        static void Main(string[] args)
        {
			#region Boards update queues
		    BoardUpdater  board = new BoardUpdater
			{
				BoardId = 1,
				UserId = 2
			};

			BoardUpdater board1 = new BoardUpdater
			{
				BoardId = 1,
				UserId = 3
			};

			BoardUpdater board2 = new BoardUpdater
			{
				BoardId = 46,
				UserId = 4
			};

			BoardUpdater board3 = new BoardUpdater
			{
				BoardId = 32,
				UserId = 5
			};

			BoardUpdater board4 = new BoardUpdater
			{
				BoardId = 32,
				UserId = 6
			};

			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board1);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board2);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board3);
			WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board4);

			Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(1).UserId);
			Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(1).UserId);
			Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(32).UserId);
			Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(46).UserId);
			Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(32).UserId);
			#endregion
			#region Notifications queuing
			//Notification notification1 = new Notification
			//{
			//	UserId = 1,
			//	Type = Notification.NotificationType.AddedToBoard,
			//	Content = "Came first in 1"
			//};

			//Notification notification2 = new Notification
			//{
			//	UserId = 1,
			//	Type = Notification.NotificationType.AddedToBoard,
			//	Content = "Came second in 1"
			//};

			//Notification notification3 = new Notification
			//{
			//	UserId = 3,
			//	Type = Notification.NotificationType.AddedToBoard,
			//	Content = "Came first in 3"
			//};

			//WeSketch.Server.NotificationsSystem.NotificationsQueue.AddToQueue(notification1);
			//WeSketch.Server.NotificationsSystem.NotificationsQueue.AddToQueue(notification2);
			//WeSketch.Server.NotificationsSystem.NotificationsQueue.AddToQueue(notification3);

			//Console.WriteLine(NotificationsQueue.RemoveFromQueue(1).Content);
			//Console.WriteLine(NotificationsQueue.RemoveFromQueue(3).Content);
			//Console.WriteLine(NotificationsQueue.RemoveFromQueue(1).Content); 
			#endregion
		}
	}
}
