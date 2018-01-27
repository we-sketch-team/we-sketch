using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;

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
            AddCollaborator();

            foreach (var item in GetBoardCollaborators(74))
            {
                Console.WriteLine(item.Id + " " + item.FirstName + " " + item.Id);
            }
        }
    }
}
