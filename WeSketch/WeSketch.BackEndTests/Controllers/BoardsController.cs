using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.Utilities;
using WeSketch.DataLayer.Model;
using WeSketch.DataLayer.UnitOfWork;

namespace WeSketch.BackEndTests.Controllers
{
    [RoutePrefix("api/boards")]
    public class BoardsController : ApiController
    {
        #region CRUD tests
        [HttpGet]
        [Route("crud/get/{id:int}")]
        public BoardDetailsDTO Get(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.GetBoard(id);
        }

        [HttpGet]
        [Route("crud/get/all")]
        public List<BoardDetailsDTO> GetAll()
        {
            DataService service = ObjectFactory.GetDataService();
            return service.AllBoards();
        }

        [HttpPost]
        [Route("crud/create")]
        public BoardDetailsDTO Create(CreateBoardDto boardDetails)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.CreateBoard(boardDetails);
        }

        [HttpPut]
        [Route("crud/update")]
        public BoardDetailsDTO Update([FromBody]BoardDetailsDTO board)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.UpdateBoard(board);
        }

        [HttpDelete]
        [Route("crud/delete/{id:int}")]
        public void Delete(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            service.DeleteBoard(id);
        }
        #endregion
        #region Business logic tests
        //TODO make DataService property of controller
        [HttpGet]
        [Route("logic/get/{id:int}")]
        public BoardDetailsDTO GetBoard(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.GetBoard(id);
        }

        [HttpGet]
        [Route("logic/get/all")]
        public List<BoardDetailsDTO> GetAllBoards()
        {
            DataService service = ObjectFactory.GetDataService();
            return service.AllBoards();
        }

        [HttpGet]
        [Route("logic/get/alluserboards/{id:int}")]
        public List<BoardDetailsDTO> GetAllUserBoards(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.GetAllUserBoards(id);
        }

        [HttpPost]
        [Route("logic/create")]
        public CreateBoardDto CreateBoard([FromBody]CreateBoardDto userBoard)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.CreateAndAttacheBoard(userBoard);
        }

        [HttpPut]
        [Route("logic/update")]
        public BoardDetailsDTO UpdateBoard([FromBody]BoardDetailsDTO board)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.UpdateBoard(board);
        }

        [HttpPut]
        [Route("logic/delete/{id:int}")]
        public void DeleteBoard(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            service.DeleteBoard(id);
        }

        [HttpPut]
        [Route("logic/setpreference")]
        public BoardDetailsDTO SetBoardPreference([FromBody]BoardPreferenceDTO preference)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.SetBoardPreference(preference.UserId, preference.BoardId);
        }
        
        [HttpGet]
        [Route("crud/userboards/{id:int}")]
        public List<UserBoards> UserBoards(int id)
        {
            UnitOfWork unitOfWork = ObjectFactory.GetUnitOfWork();
            return unitOfWork.UserRepository.GetById(id).UserBoards.ToList();            
        }       
        #endregion
    }
}
