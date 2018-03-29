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
    public static class ConverterToDTO
    {
        #region Users
        public static UserDetailsDTO UserToUserDetails(User user)
        {
            return new UserDetailsDTO
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                DateRegistered = user.DateRegistered
            };            
        }

        public static List<UserDetailsDTO> ListOfUsersToDetails(List<User> users)
        {
            List<UserDetailsDTO> result = new List<UserDetailsDTO>();

            foreach (var user in users)
            {
                result.Add(ConverterToDTO.UserToUserDetails(user));
            }

            return result;
        }
        #endregion
        #region Boards
        public static BoardDetailsDTO BoardToBoardDetails(Board board)
        {
            return new BoardDetailsDTO()
            {
                Id = board.Id,
                DateCreated = board.DateCreated,
                Title = board.Title,
                Desription = board.Desription,
                Content = board.Content,
				Password = board.Password
            };
        }

        public static CreateBoardDto BoardToCreateBoard(Board board)
        {
            return new CreateBoardDto()
            {
                DateCreated = board.DateCreated,
                Title = board.Title,
                Desription = board.Desription,
                Content = board.Content	,
				Password = board.Password
            };
        }

        public static List<BoardDetailsDTO> ListOfBoardsToDetails(List<Board> boards)
        {
            List<BoardDetailsDTO> result = new List<BoardDetailsDTO>();

            foreach (var board in boards)
            {
                BoardDetailsDTO boardDetails = ConverterToDTO.BoardToBoardDetails(board);
                result.Add(boardDetails);
            }

            return result;
        }

		public static List<BoardDetailsDTO> ListOfBoardsToBasicInformation(List<Board> boards)
		{
			List<BoardDetailsDTO> result = new List<BoardDetailsDTO>();

			foreach (var board in boards)
			{
				BoardDetailsDTO boardDetails = new BoardDetailsDTO()
				{
					Id = board.Id,
					Title = board.Title,
					Desription = board.Desription
				};
				result.Add(boardDetails);
			}

			return result;
		}
		#endregion	
    }
}
