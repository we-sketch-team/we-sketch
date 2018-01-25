using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.DTOs;

namespace WeSketch.Server.Communications.Hubs
{
    public class BoardHub : Hub
    {
        private DataService dataService = BusinessLogic.Utilities.ObjectFactory.GetDataService();

        public List<BoardDetailsDTO> GetMyBoards(int userId)
        {
            return dataService.GetAllUserBoards(userId);
        }

        public BoardDetailsDTO CreateBoard(CreateBoardDto createBoardDto)
        {
            return dataService.CreateBoard(createBoardDto);
        }

        public BoardDetailsDTO GetBoard(int boardId)
        {
            return dataService.GetBoard(boardId);
        }

        public BoardDetailsDTO UpdateBoardContent(BoardDetailsDTO boardDetailsDTO)
        {
            BoardDetailsDTO board = dataService.UpdateBoardContent(boardDetailsDTO);
            int boardId = boardDetailsDTO.Id;
            Clients.Others.GetBoard(boardId);
            return board;
        }

        public BoardDetailsDTO UpdateBoard(BoardDetailsDTO boardDetailsDTO)
        {
            BoardDetailsDTO board = dataService.UpdateBoard(boardDetailsDTO);
            int boardId = boardDetailsDTO.Id;
            Clients.Others.GetBoard(boardId);
            return board;
        }

        public void DeleteBoard(int boardId)
        {
            dataService.DeleteBoard(boardId);
        }

        public List<UserDetailsDTO> GetCollaborators(int boardId)
        {
            return dataService.GetAllBoardCollaboratros(boardId);
        }

        public void AddCollaborator(CollaboratorDTO collaboratorDTO)
        {
            dataService.AddCollaborator(collaboratorDTO);
        }
    }
}
