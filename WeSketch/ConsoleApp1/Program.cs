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
                UserId = 18,
                Desription = "again...",
                Title = "Title",
                Password = "sifrica"
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

            dataService.RemoveCollaborator(collaboratorDTO);
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
			SyncerData data = new SyncerData()
			{
						
			};

			SyncerDataModifier modifier = SynhronizerModifierFactory.GetCreateActionModifier(data);
			CommonBoard board = new CommonBoard()
			{
				UserId = 18,
				Content = "now this works fine twice..",
				Title = "titled"
			};

			modifier.Modify(board);
			data = modifier.GetModifiedData();

			CommonBoard board1 = new CommonBoard()
			{
				UserId = 18,
				Content = "now this works fine hee heee..",
				Title = "titled",
				BoardId = 141
			};

			modifier = SynhronizerModifierFactory.GetUpdateActionModifier(data);
			modifier.Modify(board1);

			data = modifier.GetModifiedData();

			dataService.SyncOfllineMode(data);
		}

        static void Main(string[] args)
        {
			TestSyncOflline();
		}
	}
}
