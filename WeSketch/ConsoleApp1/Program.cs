using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.Server.NotificationsSystem;

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
			//BoardDetailsDTO board = new BoardDetailsDTO
			//{
			//	Id = 1,
			//	Content = "this came firts in number 1 board" 
			//};

			//BoardDetailsDTO board1 = new BoardDetailsDTO
			//{
			//	Id = 1,
			//	Content = "this came second in number 1 board"
			//};

			//BoardDetailsDTO board2 = new BoardDetailsDTO
			//{
			//	Id = 46,
			//	Content = "this came firts in number 46 board"
			//};

			//BoardDetailsDTO board3 = new BoardDetailsDTO
			//{
			//	Id = 32,
			//	Content = "this came firts in number 32 board"
			//};

			//BoardDetailsDTO board4 = new BoardDetailsDTO
			//{
			//	Id = 32,
			//	Content = "this came second in number 32 board"
			//};

			//WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board);
			//WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board1);
			//WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board2);
			//WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board3);
			//WeSketch.Server.Queues.BoardsUpdateQueue.AddToQueue(board4);

			//Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(1).Content);
			//Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(1).Content);
			//Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(32).Content);
			//Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(46).Content);
			//Console.WriteLine(WeSketch.Server.Queues.BoardsUpdateQueue.RemoveFromQueue(32).Content);
			#endregion

			Notification notification1 = new Notification
			{
				UserId = 1,
				Type = Notification.NotificationType.AddedToBoard,
				Content = "Came first in 1"
			};

			Notification notification2 = new Notification
			{
				UserId = 1,
				Type = Notification.NotificationType.AddedToBoard,
				Content = "Came second in 1"
			};

			Notification notification3 = new Notification
			{
				UserId = 3,
				Type = Notification.NotificationType.AddedToBoard,
				Content = "Came first in 3"
			};

			WeSketch.Server.NotificationsSystem.NotificationsQueue.AddToQueue(notification1);
			WeSketch.Server.NotificationsSystem.NotificationsQueue.AddToQueue(notification2);
			WeSketch.Server.NotificationsSystem.NotificationsQueue.AddToQueue(notification3);

			Console.WriteLine(NotificationsQueue.RemoveFromQueue(1).Content);
			Console.WriteLine(NotificationsQueue.RemoveFromQueue(3).Content);
			Console.WriteLine(NotificationsQueue.RemoveFromQueue(1).Content);
		}
	}
}
