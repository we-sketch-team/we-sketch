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
        public bool PublicBoard { get; set; }
        public string Title { get; set; }
        public string Desription { get; set; }
        public string Content { get; set; }
        public bool IsFavoriteToUser { get; set; }
    }
}
