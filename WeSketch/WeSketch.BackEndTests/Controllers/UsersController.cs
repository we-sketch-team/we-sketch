using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeSketch.DataLayer.Model;
using WeSketch.DataLayer.UnitOfWork;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.Utilities;

namespace WeSketch.BackEndTests.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        #region CRUD tests
        [Route("crud/all")]
        [HttpGet]
        public List<UserDetailsDTO> GetAll()
        {
            DataService service = ObjectFactory.GetDataService();
            return service.GetAllUsers();
        }

        [HttpGet]
        [Route("crud/byid/{id:int}")]
        public UserDetailsDTO GetById(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.GetUser(id);
        }

        [HttpGet]
        [Route("crud/byusername/{username}")]
        public UserDetailsDTO GetByUsername(string username)
        {
            UnitOfWork unitOfWork = ObjectFactory.GetUnitOfWork();
            User user = unitOfWork.UserRepository.GetByUsername(username);
            return ConverterToDTO.UserToUserDetails(user);
        }


        //Example : http://localhost:63605/api/users/crud/byemail/?email=hochopepa@gmail.com
        [HttpGet]
        [Route("crud/byemail")]
        public UserDetailsDTO GetByEmail([FromUri] string email)
        {
            UnitOfWork unitOfWork = ObjectFactory.GetUnitOfWork();
            User user = unitOfWork.UserRepository.GetByEmail(email);
            return ConverterToDTO.UserToUserDetails(user);
        }

        [HttpPost]
        [Route("crud/insert")]
        public UserDetailsDTO InsertUser([FromBody] CreateUserDTO user)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.CreateAccount(user);
        }

        [HttpPut]
        [Route("crud/update")]
        public UserDetailsDTO Update([FromBody] UserDetailsDTO userDetails)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.UpdateUser(userDetails);
        }

        [HttpDelete]
        [Route("crud/delete/{id:int}")]
        public void Delete(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            service.DeleteUser(id);
        }
        #endregion
        #region Business logic tests
        //TODO make DataService property of controller
        [HttpGet]
        [Route("logic/get/{id:int}")]
        public UserDetailsDTO GetUser(int id)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.GetUser(id);
        }

        [HttpGet]
        [Route("logic/get/all")]
        public List<UserDetailsDTO> GetAllUsers()
        {
            DataService service = ObjectFactory.GetDataService();
            return service.GetAllUsers();
        }

        [HttpPost]
        [Route("logic/login")]
        public UserDetailsDTO Login([FromBody]LoginDTO login)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.Login(login);
        }

        [HttpPost]
        [Route("logic/createaccount")]
        public UserDetailsDTO CreateAccount([FromBody]CreateUserDTO userDetails)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.CreateAccount(userDetails);
        }

        [HttpPut]
        [Route("logic/update")]
        public UserDetailsDTO UpdateUser([FromBody]UserDetailsDTO userDetails)
        {
            DataService service = ObjectFactory.GetDataService();
            return service.UpdateUser(userDetails);
        }
        #endregion
    }
}
