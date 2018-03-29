using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.BusinessLogic.DTOs.BoardDTOs
{
    public class CreateBoardDto
    {
        public DateTime DateCreated { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Desription { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}
