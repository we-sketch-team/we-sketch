using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;

namespace WeSketch.BusinessLogic.Utilities
{
    public static class InvalidDTOFactory
    {
        public static UserDetailsDTO InvalidUser()
        {
            return new UserDetailsDTO() { Id = -1 };
        }  
        
        public static BoardDetailsDTO InvalidBoard()
        {
            return new BoardDetailsDTO() { Id = -1 };
        }

        public static CreateBoardDto InvalidCreateBoard()
        {
            return new CreateBoardDto() { UserId = -1 };
        }
    }
}
