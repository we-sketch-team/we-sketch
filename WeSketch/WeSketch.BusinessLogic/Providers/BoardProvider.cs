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

            foreach (var userBoard in board.UserBoards.ToList())
            {
                collaborators.Add(userBoard.User);
            }

            return ConverterToDTO.ListOfUsersToDetails(collaborators);            
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
           
            List<UserBoards> userBoard = user.UserBoards.ToList();
            List<BoardDetailsDTO> result = new List<BoardDetailsDTO>();
            BoardDetailsDTO boardDetails;

            foreach (var board in userBoard)
            {
                boardDetails = ConverterToDTO.BoardToBoardDetails(board.Board);
                boardDetails.IsFavoriteToUser = Utility.IsFavorite(user, board.Board);
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

        public List<BoardDetailsDTO> AllBoards()
        {     
            List<Board> boards = unitOfWork.BoardRepository.GetAll();

            return ConverterToDTO.ListOfBoardsToDetails(boards);
        }

        public CreateBoardDto CreateAndAttacheBoard(CreateBoardDto createBoards)
        {
            User boardCreater = mediator.User;

            if (boardCreater == null)
                return InvalidDTOFactory.InvalidCreateBoard();

            Board board = new Board();
            board = ConverterFromDTO.BoardFromCreateBoard(createBoards);
            board.DateCreated = DateTime.Now;
            board.ActiveBoard = true;
            unitOfWork.BoardRepository.Insert(board);
            unitOfWork.Save();

            UserBoards userBoards = new UserBoards();
            userBoards.BoardId = board.Id;
            userBoards.Board = board;
            userBoards.User = boardCreater;
            userBoards.UserId = boardCreater.Id;
            userBoards.Role = "Creater"; //TODO do not hardcode

            boardCreater.UserBoards.Add(userBoards);
            board.UserBoards.Add(userBoards);
            board.UserBoards.Add(userBoards);
            boardCreater.UserBoards.Add(userBoards);
            unitOfWork.Save();

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.UserRepository.Update(boardCreater);
            unitOfWork.Save();

            CreateBoardDto result = ConverterToDTO.BoardToCreateBoard(board);
            result.UserId = boardCreater.Id;         

            return result;
        }

        public BoardDetailsDTO CreateBoard(CreateBoardDto createBoards)
        {          

            Board board = new Board();
            board = ConverterFromDTO.BoardFromCreateBoard(createBoards);
            board.DateCreated = DateTime.Now;
            board.ActiveBoard = true;
            unitOfWork.BoardRepository.Insert(board);
            unitOfWork.Save();

            return ConverterToDTO.BoardToBoardDetails(board);
        }

        public BoardDetailsDTO SetBoardPreference(int boardId)
        {
            User user = mediator.User;

            if (user == null)
                return InvalidDTOFactory.InvalidBoard();

            Board board = unitOfWork.BoardRepository.GetById(boardId);

            if(board == null)
                return InvalidDTOFactory.InvalidBoard();

            user.UserBoards.ToList().Find(x => x.BoardId == boardId).IsFavoriteToUser = !user.UserBoards.ToList().Find(x => x.BoardId == boardId).IsFavoriteToUser;

            unitOfWork.Save();

            BoardDetailsDTO updatedBoard =  ConverterToDTO.BoardToBoardDetails(board);
            updatedBoard.IsFavoriteToUser = Utility.IsFavorite(user, board);

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
            User user = mediator.User;

            UserBoards userBoard = new UserBoards();
            userBoard.Board = board;
            userBoard.BoardId = boardId;
            userBoard.User = user;
            userBoard.UserId = userId;
            userBoard.Role = "Collaborator";

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
            User user = mediator.User;

            UserBoards userBoards = board.UserBoards.ToList().First(x => x.UserId == userId);

            board.UserBoards.Remove(userBoards);

            unitOfWork.BoardRepository.Update(board);
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
        }

    }
}
