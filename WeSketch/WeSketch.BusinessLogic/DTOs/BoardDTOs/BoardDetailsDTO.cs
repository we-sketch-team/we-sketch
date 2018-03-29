using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.BusinessLogic.DTOs.BoardDTOs
{
    public class BoardDetailsDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Desription { get; set; }
        public string Content { get; set; }
        public bool IsFavoriteToUser { get; set; }
        public string Role { get; set; }
    }
}
