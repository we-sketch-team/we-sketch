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
using WeSketch.Common.CommonClasses;

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
                UserId = 17,
                Desription = "again...",
                Title = "Title",
                Password = ""
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

		public static void TestSyncOflline()
		{
			List<CommonBoard> boardsToCreate = new List<CommonBoard>();
			boardsToCreate.Add(new CommonBoard()
			{
				Content = "this is new",
				Description = "described",
				Title = "this is cool title",
				UserId = 4
			});


			List<CommonBoard> boardsToUpdate = new List<CommonBoard>();
			boardsToUpdate.Add(new CommonBoard()
			{
				BoardId = 100,
				Content = "hell yeah!",
				Description = "hell yeah!",
				Title = "hell yeah title",
				UserId = 2
			});

			SyncerData data = new SyncerData()
			{
				BoardsToCreate = boardsToCreate,
				BoardsToUpdate = boardsToUpdate				
			};

			data.BoardsToDelete.Add(106);

			dataService.SyncOfllineMode(data);
		}

        static void Main(string[] args)
        {

			List<BoardDetailsDTO> board = dataService.GetBoardsSharedWithUser(17);
			Console.WriteLine("Check it out now..");
		}
	}
}
