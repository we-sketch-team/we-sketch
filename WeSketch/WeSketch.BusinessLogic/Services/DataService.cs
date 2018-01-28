﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.Providers;
using WeSketch.BusinessLogic.Utilities;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.BusinessLogic.DTOs.ChatRoomDTOs;
using WeSketch.BusinessLogic.DTOs.MessageDTOs;

namespace WeSketch.BusinessLogic.Services
{
    public class DataService
    {
        private BoardProvider boardProvider;
        private ChatRoomProvider chatRoomProvider;
        private UserProvider userProvider;
        private Mediator mediator;

        public DataService()
        {
            mediator = ObjectFactory.GetMediator();
            boardProvider = ObjectFactory.GetBoardProvider(mediator);
            chatRoomProvider = ObjectFactory.GetChatRoomProvider(mediator);
            userProvider = ObjectFactory.GetUserProvider(mediator);
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

        public BoardDetailsDTO GetBoard(int id)
        {
            return boardProvider.GetBoard(id);
        }

        public UserDetailsDTO UpdateUser(UserDetailsDTO userDetails)
        {
            return userProvider.UpdateUser(userDetails);
        }

        public CreateBoardDto CreateBoard(CreateBoardDto userBoard)
        {
            int userId = userBoard.UserId;
            userProvider.SetMediatorUser(userId);
            chatRoomProvider.CreateChatRoom();
            return boardProvider.CreateBoard(userBoard);
        }        

        public List<BoardDetailsDTO> GetAllPublicBoards()
        {
            return boardProvider.GetAllPublicBoards();
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

        public ChatRoomDetailsDTO UpdateChatRoom(UpdateChatRoomDTO updateChatRoom)
        {
            return chatRoomProvider.UpdateChatRoom(updateChatRoom);
        }

        public void DeleteChatRoom(int id)
        {
            chatRoomProvider.DeleteChatRoom(id);
        }        

        public void AddCollaborator(CollaboratorDTO collaboratorDTO)
        {
            userProvider.SetMediatorUser(collaboratorDTO.UserId);
            boardProvider.AddCollaborator(collaboratorDTO);
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
    }
}
