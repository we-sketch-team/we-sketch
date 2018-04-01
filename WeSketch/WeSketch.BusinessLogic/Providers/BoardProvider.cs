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
using WeSketch.Common.CommonClasses;

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

        public List<BoardDetailsDTO> GetAllBoards(int userId)
        {
			List<Board> boards = unitOfWork.BoardRepository.GetAll();
			List<Board> toRemoveList = new List<Board>();
			foreach (var board in boards)
			{
				UserBoards match = board.UserBoards.ToList().Find(x => x.UserId == userId && x.Role == Utility.CreatorRole());

				if (match == null)
					continue;

				toRemoveList.Add(board);
			}
			foreach (var board in toRemoveList)
			{
				var toToRemove = boards.Find(x => x.Id == board.Id);
				boards.Remove(toToRemove);
			}
			return ConverterToDTO.ListOfBoardsToDetails(boards);
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

			foreach (var userBoard in userBoards)
			{
				if (userBoard.Role != Utility.CreatorRole())
					continue;

				BoardDetailsDTO boardDetails = new BoardDetailsDTO();

				boardDetails.Title = userBoard.Board.Title;
				boardDetails.Id = userBoard.BoardId;
				boardDetails.Desription = userBoard.Board.Desription;
				boardDetails.Password = userBoard.Board.Password;
				boardDetails.IsPasswordProtected = Utility.IsPasswordProtected(boardDetails.Password);
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
				boardDetails.Password = userBoard.Board.Password;
				boardDetails.IsPasswordProtected = Utility.IsPasswordProtected(boardDetails.Password);
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

            BoardDetailsDTO result = ConverterToDTO.BoardToBoardDetails(board);
			result.IsPasswordProtected = Utility.IsPasswordProtected(result.Password);

			return result;
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
			result.IsPasswordProtected = Utility.IsPasswordProtected(result.Password);
			return result;
		}	

        public CreateBoardDto CreateBoard(CreateBoardDto createBoards)
        {
            User boardCreater = mediator.User;

            if (boardCreater == null)
                return InvalidDTOFactory.InvalidCreateBoard();

            createBoards.Password = createBoards.Password ?? string.Empty;

            Board board = StoreBoardToDatabase(createBoards);
            UserBoards userBoards = ConnectBoardAndUser(boardCreater, board);
            userBoards.Role = Utility.CreatorRole();
            AttachBoardToUser(userBoards, board);

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
            board.Desription = boardDetails.Desription;
			board.Password = boardDetails.Password;

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.Save();

            return ConverterToDTO.BoardToBoardDetails(board);
        }

        public bool AddCollaborator(CollaboratorDTO collaboratorDTO)
        {
            int boardId = collaboratorDTO.BoardId;
            int userId = collaboratorDTO.UserId;

            Board board = unitOfWork.BoardRepository.GetById(boardId);

            if (board == null)
                return false;

			string pass = collaboratorDTO.Password == null ? string.Empty : collaboratorDTO.Password;
            string databasePass = board.Password == null ? string.Empty : board.Password;

            if (databasePass != pass)
				return false;

            User user = mediator.User;

			UserBoards isCreater = user.UserBoards.ToList().Find(x => x.BoardId == boardId && x.Role == Utility.CreatorRole());

			if (isCreater != null)
				return false;

            if (user == null)
                return false;

            UserBoards userBoard = ConnectBoardAndUser(user, board);
            userBoard.Role = Utility.CollaboratorRole();

            user.UserBoards.Add(userBoard);
            board.UserBoards.Add(userBoard);

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.UserRepository.Update(user);

            unitOfWork.Save();

            return true;
        }

        public void RemoveCollaborator(CollaboratorDTO collaboratorDTO)
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

			if (userBoards == null)
				return;

			if (userBoards.Role != Utility.CollaboratorRole())
				return;

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

        public List<BoardDetailsDTO> GetSharedBoardsWithUser()
        {
            var user = mediator.User;
            var userBoards = user.UserBoards;
            var list = new List<BoardDetailsDTO>();
            foreach(var ub in userBoards)
            {
                if (ub.Role == Utilities.Utility.CreatorRole())
                    continue;
                list.Add(new BoardDetailsDTO()
                {
                    Id = ub.BoardId,
                    Title = ub.Board.Title,
                    Desription = ub.Board.Desription,
					IsPasswordProtected = Utility.IsPasswordProtected(ub.Board.Password)
                });
            }

            return list;
        }

		public void SyncDeletedBoards(List<CommonBoard> ids)
		{
			foreach (var id in ids)
			{
				unitOfWork.BoardRepository.Delete(id.BoardId);
			}
			unitOfWork.Save();
		}

		public void SyncUpdatedBoards(List<CommonBoard> boards)
		{
			foreach (var boardToUpdate in boards)
			{
				Board board = unitOfWork.BoardRepository.GetById(boardToUpdate.BoardId);
				board.Content = boardToUpdate.Content;
				board.Desription = boardToUpdate.Description;
				board.Title = boardToUpdate.Title;
				board.ActiveBoard = true;
				unitOfWork.BoardRepository.Update(board);
			}
			unitOfWork.Save();
		}

		public void SyncCreatedBoards(List<CommonBoard> boards)
		{
			foreach (var boardToCreate in boards)
			{
				Board board = new Board()
				{
					Content = boardToCreate.Content,
					Title = boardToCreate.Title,
					Desription = boardToCreate.Description,
					DateCreated = DateTime.Now,
					ActiveBoard = true
				};
				unitOfWork.BoardRepository.Insert(board);
				unitOfWork.Save();
				User user = unitOfWork.UserRepository.GetById(boardToCreate.UserId);
				UserBoards userBoards = ConnectBoardAndUser(user, board);
				userBoards.Role = Utility.CreatorRole();
				user.UserBoards.Add(userBoards);
				board.UserBoards.Add(userBoards);
				board.UserBoards.Add(userBoards);
				user.UserBoards.Add(userBoards);
				unitOfWork.Save();

				unitOfWork.BoardRepository.Update(board);
				unitOfWork.UserRepository.Update(user);
			}
			unitOfWork.Save();
		}
	}
}
