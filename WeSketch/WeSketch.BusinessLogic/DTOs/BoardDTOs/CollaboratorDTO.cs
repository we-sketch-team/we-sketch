using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.BusinessLogic.DTOs.BoardDTOs
{
    public class CollaboratorDTO
    {
        public int UserId { get; set; }
        public int BoardId { get; set; }
		public string Password { get; set; }
	}
}
