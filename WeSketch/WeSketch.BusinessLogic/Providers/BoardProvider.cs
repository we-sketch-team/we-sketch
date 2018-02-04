using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Utilities;
using WeSketch.DataLayer.UnitOfWork;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.DataLayer.Model;

namespace WeSketch.BusinessLogic.Providers
{
    public class BoardProvider : IDisposable
    {
        private Mediator mediator;
        private UnitOfWork unitOfWork;        

        public BoardProvider(Mediator mediator)
        {
            this.mediator = mediator;
            unitOfWork = ObjectFactory.GetUnitOfWork();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void SetMediatorBoard(int id)
        {
            Board board = unitOfWork.BoardRepository.GetById(id);
            this.mediator.Board = board;
        }

        public List<UserDetailsDTO> GetAllBoardCollaboratros(int id)
        {
            List<User> collaborators = new List<User>();
            Board board = unitOfWork.BoardRepository.GetById(id);

            if(board == null)
            {
                List<UserDetailsDTO> invalidResult = new List<UserDetailsDTO>();
                invalidResult.Add(InvalidDTOFactory.InvalidUser());
                return invalidResult;
            }

            foreach (var userBoard in board.UserBoards.ToList())
            {
                collaborators.Add(userBoard.User);
            }

            return ConverterToDTO.ListOfUsersToDetails(collaborators);            
        }

		public List<BoardDetailsDTO> GetMyBoardsBasicInformation()
		{
			User user = mediator.User;

			if (user == null)
			{
				List<BoardDetailsDTO> invalidResult = new List<BoardDetailsDTO>();
				invalidResult.Add(InvalidDTOFactory.InvalidBoard());
				return invalidResult;
			}

			List<UserBoards> userBoards = user.UserBoards.ToList();
			List<BoardDetailsDTO> result = new List<BoardDetailsDTO>();
			BoardDetailsDTO boardDetails =  new BoardDetailsDTO();

			foreach (var userBoard in userBoards)
			{
				if (userBoard.Role != Utility.CreatorRole())
					continue;

				boardDetails.Title = userBoard.Board.Title;
				boardDetails.Id = userBoard.BoardId;
				boardDetails.Desription = userBoard.Board.Desription;
				result.Add(boardDetails);
			}

			return result;
		}

		public List<BoardDetailsDTO> GetAllUserBoards()
        {
            User user = mediator.User;

            if(user == null)
            {
                List<BoardDetailsDTO> invalidResult = new List<BoardDetailsDTO>();
                invalidResult.Add(InvalidDTOFactory.InvalidBoard());
                return invalidResult;
            }
           
            List<UserBoards> userBoards = user.UserBoards.ToList();
            List<BoardDetailsDTO> result = new List<BoardDetailsDTO>();
            BoardDetailsDTO boardDetails;

            foreach (var userBoard in userBoards)
            {
                boardDetails = ConverterToDTO.BoardToBoardDetails(userBoard.Board);
                boardDetails.IsFavoriteToUser = userBoard.IsFavoriteToUser;
                boardDetails.Role = userBoard.Role;
                result.Add(boardDetails);
            }

            return result;
        }            

        public BoardDetailsDTO GetBoard(int id)
        {
            if (id <= 0)
                return InvalidDTOFactory.InvalidBoard();

            Board board = unitOfWork.BoardRepository.GetById(id);

            if (board == null)
                return InvalidDTOFactory.InvalidBoard();

            return ConverterToDTO.BoardToBoardDetails(board); 
        }

		public BoardDetailsDTO GetBoadWithRole(int boardId)
		{
			if (boardId <= 0)
				return InvalidDTOFactory.InvalidBoard();

			Board board = unitOfWork.BoardRepository.GetById(boardId);

			if (board == null)
				return InvalidDTOFactory.InvalidBoard();

			BoardDetailsDTO result = ConverterToDTO.BoardToBoardDetails(board);
			result.Role = Utility.GetRole(mediator.User, board);
			return result;
		}

		public List<BoardDetailsDTO> GetAllPublicBoards()
        {     
            List<Board> boards = unitOfWork.BoardRepository.GetAll().FindAll(x => x.PublicBoard);

            return ConverterToDTO.ListOfBoardsToDetails(boards);
        }

        public CreateBoardDto CreateBoard(CreateBoardDto createBoards)
        {
            User boardCreater = mediator.User;

            if (boardCreater == null)
                return InvalidDTOFactory.InvalidCreateBoard();

            Board board = StoreBoardToDatabase(createBoards);
            UserBoards userBoards = ConnectBoardAndUser(boardCreater, board);
            userBoards.Role = Utility.CreatorRole();
            AttachBoardToUser(userBoards, board);
            AttachChatRoom(board);

            CreateBoardDto result = ConverterToDTO.BoardToCreateBoard(board);
            result.UserId = boardCreater.Id;         

            return result;
        }

        public Board StoreBoardToDatabase(CreateBoardDto createBoards)
        {
            Board board = new Board();
            board = ConverterFromDTO.BoardFromCreateBoard(createBoards);
            board.DateCreated = DateTime.Now;
            board.ActiveBoard = true;
            unitOfWork.BoardRepository.Insert(board);
            unitOfWork.Save();

            return board;
        }

        public UserBoards ConnectBoardAndUser(User user, Board board)
        {
            UserBoards userBoards = new UserBoards();
            userBoards.BoardId = board.Id;
            userBoards.Board = board;
            userBoards.User = user;
            userBoards.UserId = user.Id;

            return userBoards;
        }

        public void AttachBoardToUser(UserBoards userBoards, Board board)
        {
            User boardCreater = mediator.User;

            boardCreater.UserBoards.Add(userBoards);
            board.UserBoards.Add(userBoards);
            board.UserBoards.Add(userBoards);
            boardCreater.UserBoards.Add(userBoards);
            unitOfWork.Save();

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.UserRepository.Update(boardCreater);
            unitOfWork.Save();
        }

        public void AttachChatRoom(Board board)
        {
            ChatRoom chatRoom = mediator.ChatRoom;
            User user = mediator.User;
            board.ChatRoom = chatRoom;
            user.ChatRooms.Add(chatRoom);

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.ChatRoomRepository.Update(chatRoom);
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
        }

        public BoardDetailsDTO SetBoardPreference(BoardPreferenceDTO boardPreferenceDTO)
        {
            User user = mediator.User;

            if (user == null)
                return InvalidDTOFactory.InvalidBoard();

            UserBoards userBoard = user.UserBoards.ToList().Find(x => x.BoardId == boardPreferenceDTO.BoardId);

            userBoard.IsFavoriteToUser = boardPreferenceDTO.IsFavorite;
            unitOfWork.Save();

            Board board = userBoard.Board;

            BoardDetailsDTO updatedBoard =  ConverterToDTO.BoardToBoardDetails(board);

            return updatedBoard;
        }

        public void DeleteBoard(int id)
        {
            unitOfWork.BoardRepository.Delete(id);
            unitOfWork.Save();            
        }

        public BoardDetailsDTO UpdateBoard(BoardDetailsDTO boardDetails)
        {
            Board board = unitOfWork.BoardRepository.GetById(boardDetails.Id);

            if (board == null)
                return Utilities.InvalidDTOFactory.InvalidBoard();

            board.Content = boardDetails.Content;
            board.Title = boardDetails.Title;
            board.PublicBoard = boardDetails.PublicBoard;
            board.Desription = boardDetails.Desription;

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.Save();

            return ConverterToDTO.BoardToBoardDetails(board);
        }

        public void AddCollaborator(CollaboratorDTO collaboratorDTO)
        {
            int boardId = collaboratorDTO.BoardId;
            int userId = collaboratorDTO.UserId;

            Board board = unitOfWork.BoardRepository.GetById(boardId);

            if (board == null)
                return;

            User user = mediator.User;

            if (user == null)
                return;

            UserBoards userBoard = ConnectBoardAndUser(user, board);
            userBoard.Role = Utility.CollaboratorRole();

            user.UserBoards.Add(userBoard);
            board.UserBoards.Add(userBoard);

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.UserRepository.Update(user);

            unitOfWork.Save();
        }

        public void RemoveCollaboratro(CollaboratorDTO collaboratorDTO)
        {
            int boardId = collaboratorDTO.BoardId;
            int userId = collaboratorDTO.UserId;

            Board board = unitOfWork.BoardRepository.GetById(boardId);

            if (board == null)
                return;

            User user = mediator.User;

            if (user == null)
                return;

            UserBoards userBoards = board.UserBoards.ToList().First(x => x.UserId == userId);

            board.UserBoards.Remove(userBoards);

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
        }

        public BoardDetailsDTO UpdateBoardContent(BoardDetailsDTO boardDetailsDTO)
        {
            string content = boardDetailsDTO.Content;

            Board board = unitOfWork.BoardRepository.GetById(boardDetailsDTO.Id);

            if (board == null)
                return Utilities.InvalidDTOFactory.InvalidBoard();

            board.Content = content;

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.Save();

            return boardDetailsDTO;
        }
    }
}
