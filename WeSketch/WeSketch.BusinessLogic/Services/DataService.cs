using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Providers;
using WeSketch.BusinessLogic.Utilities;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.Common.CommonClasses;

namespace WeSketch.BusinessLogic.Services
{
    public class DataService
    {
        private BoardProvider boardProvider;
        private UserProvider userProvider;
        private Mediator mediator;

        public DataService()
        {
            mediator = ObjectFactory.GetMediator();
            boardProvider = ObjectFactory.GetBoardProvider(mediator);
            userProvider = ObjectFactory.GetUserProvider(mediator);
        }

        public List<BoardDetailsDTO> GetAllBoards(int userId)
        {
            return boardProvider.GetAllBoards(userId);
        }

        public UserDetailsDTO Login(LoginDTO login)
        {
            return userProvider.Login(login);
        }

        public UserDetailsDTO CreateAccount(CreateUserDTO userDetails)
        {
            return userProvider.CreateAccount(userDetails);
        }

        public UserDetailsDTO GetUser(int id)
        {
            return userProvider.GetUser(id);
        }

        public List<UserDetailsDTO> GetAllUsers()
        {
            return userProvider.GetAllUsers();
        }

        public List<BoardDetailsDTO> GetAllUserBoards(int id)
        {
            userProvider.SetMediatorUser(id);
            return boardProvider.GetAllUserBoards();
        }

		public List<BoardDetailsDTO> GetMyBoardsBasicInformation(int userId)
		{
			userProvider.SetMediatorUser(userId);
			return boardProvider.GetMyBoardsBasicInformation();
		}

		public BoardDetailsDTO GetBoard(int id)
        {
            return boardProvider.GetBoard(id);
        }

		public BoardDetailsDTO GetBoardWithRole(int userId, int boardId)
		{
			userProvider.SetMediatorUser(userId);
			return boardProvider.GetBoadWithRole(boardId);
		}

		public UserDetailsDTO UpdateUser(UserDetailsDTO userDetails)
        {
            return userProvider.UpdateUser(userDetails);
        }

        public CreateBoardDto CreateBoard(CreateBoardDto userBoard)
        {
            int userId = userBoard.UserId;
            userProvider.SetMediatorUser(userId);
            return boardProvider.CreateBoard(userBoard);
        }      

        public BoardDetailsDTO SetBoardPreference(BoardPreferenceDTO boardPreferenceDTO)
        {
            userProvider.SetMediatorUser(boardPreferenceDTO.UserId);
            return boardProvider.SetBoardPreference(boardPreferenceDTO);
        }

        public BoardDetailsDTO UpdateBoard(BoardDetailsDTO boardDetails)
        {
            return boardProvider.UpdateBoard(boardDetails);
        }

        public void DeleteBoard(int id)
        {
            boardProvider.DeleteBoard(id);
        } 

        public void DeleteUser(int id)
        {
            userProvider.DeleteUser(id);
        }            

        public bool AddCollaborator(CollaboratorDTO collaboratorDTO)
        {
            userProvider.SetMediatorUser(collaboratorDTO.UserId);
            return boardProvider.AddCollaborator(collaboratorDTO);
        }

        public List<UserDetailsDTO> GetAllBoardCollaboratros(int id)
        {
            return boardProvider.GetAllBoardCollaboratros(id);
        }

        public void RemoveCollaboratro(CollaboratorDTO collaboratorDTO)
        {
            userProvider.SetMediatorUser(collaboratorDTO.UserId);
            boardProvider.RemoveCollaboratro(collaboratorDTO);
        }

        public BoardDetailsDTO UpdateBoardContent(BoardDetailsDTO boardDetailsDTO)
        {
            return boardProvider.UpdateBoardContent(boardDetailsDTO);
        }

        public UserDetailsDTO GetUserByUsername(string username)
        {
            return userProvider.GetUserByUsername(username);
        }

        public List<BoardDetailsDTO> GetBoardsSharedWithUser(int userId)
        {
            userProvider.SetMediatorUser(userId);
            return boardProvider.GetSharedBoardsWithUser();
        }

		public void SyncOfllineMode(SyncerData data)
		{
			Syncer syncer = new Syncer(data);
			syncer.Sync();
		}
    }
}
