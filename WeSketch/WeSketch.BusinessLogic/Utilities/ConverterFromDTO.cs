using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.DataLayer.Model;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;

namespace WeSketch.BusinessLogic.Utilities
{
    public static class ConverterFromDTO
    {
        public static User UserFromCreateUser(CreateUserDTO createUser)
        {
            return new User
            {
                Username = createUser.Username,
                Email = createUser.Email,
                Password = createUser.Password,
                DateOfBirth = createUser.DateOfBirth,
                FirstName = createUser.FirstName,
                LastName = createUser.LastName
            };
        }

        public static User UserFromUserDetails(UserDetailsDTO userDetails)
        {
            return new User
            {
                Username = userDetails.Username,
                Email = userDetails.Email,
                DateOfBirth = userDetails.DateOfBirth,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Id = userDetails.Id,
                DateRegistered = userDetails.DateRegistered
            };
        }

        public static Board BoardFromBoardDetails(BoardDetailsDTO boardDetails)
        {
            return new Board
            {
                Id = boardDetails.Id,
                Content = boardDetails.Content,
                DateCreated = boardDetails.DateCreated,
                PublicBoard = boardDetails.PublicBoard,
                Title = boardDetails.Title,
                Desription = boardDetails.Desription
            };
        }

        public static Board BoardFromCreateBoard(CreateBoardDto createBoard)
        {
            return new Board
            {
                Content = createBoard.Content,
                DateCreated = createBoard.DateCreated,
                PublicBoard = createBoard.PublicBoard,
                Title = createBoard.Title,
                Desription = createBoard.Desription
            };
        }
    }
}
