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

        static void Main(string[] args)
        {
            foreach (var item in GetBoardCollaborators(69))
            {
                Console.WriteLine(item.Email + " " + item.FirstName + " " + item.Id);
            }
        }
    }
}
