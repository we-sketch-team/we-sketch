using System;
using System.Collections.Generic;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;
using WeSketch.BusinessLogic.Utilities;
using WeSketch.DataLayer.Model;
using WeSketch.DataLayer.UnitOfWork;

namespace WeSketch.BusinessLogic.Providers
{
    public class UserProvider : IDisposable
    {
        private Mediator mediator;
        private UnitOfWork unitOfWork;       

        public UserProvider(Mediator mediator)
        {
            this.mediator = mediator;   
            unitOfWork = ObjectFactory.GetUnitOfWork();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void SetMediatorUser(int id)
        {      
            User user = unitOfWork.UserRepository.GetById(id);
            this.mediator.User = user;
        }

        public UserDetailsDTO Login(LoginDTO login)
        {            
            if (login.Email == null)
                return InvalidDTOFactory.InvalidUser();

            if (login.Password == null)
                return InvalidDTOFactory.InvalidUser();

            string email = login.Email;
            User user = unitOfWork.UserRepository.GetByEmail(email);

            if (user== null)
                return InvalidDTOFactory.InvalidUser();

            string password = login.Password;

            if(!(password == user.Password))
            {
                return InvalidDTOFactory.InvalidUser();
            }

            return ConverterToDTO.UserToUserDetails(user);
        }       

        public UserDetailsDTO CreateAccount(CreateUserDTO userDetails)
        {
            if(userDetails.Email == null)
                return InvalidDTOFactory.InvalidUser();

            if (userDetails.Username == null)
                return InvalidDTOFactory.InvalidUser();


            User userByUsername;
            string username = userDetails.Username;
            string email = userDetails.Email;

            userByUsername = unitOfWork.UserRepository.GetByUsername(username);

            if(userByUsername != null)
            {
                return InvalidDTOFactory.InvalidUser();
            }

            User userByEmail = unitOfWork.UserRepository.GetByEmail(email);

            if(userByEmail != null)
            {
                return InvalidDTOFactory.InvalidUser();
            }

            if (userDetails.DateOfBirth.Year < 1900)
            {
                userDetails.DateOfBirth = DateTime.Now;
            }

            User createUser = ConverterFromDTO.UserFromCreateUser(userDetails);


            createUser.DateRegistered = DateTime.Now;
            createUser.ActiveAccount = true;

            unitOfWork.UserRepository.Insert(createUser);
            unitOfWork.Save();

            User loggedUser = unitOfWork.UserRepository.GetByUsername(createUser.Username);
            return ConverterToDTO.UserToUserDetails(loggedUser);            
        }

        public UserDetailsDTO GetUser(int id)
        {
            if (id <= 0)
                return InvalidDTOFactory.InvalidUser();

            User user = unitOfWork.UserRepository.GetById(id);

            if(user == null)
                return InvalidDTOFactory.InvalidUser();

            return ConverterToDTO.UserToUserDetails(user);
        }

        public List<UserDetailsDTO> GetAllUsers()
        {
            List<User> users = unitOfWork.UserRepository.GetAll();

            return ConverterToDTO.ListOfUsersToDetails(users);
        }     

        public UserDetailsDTO UpdateUser(UserDetailsDTO userDetails)
        {
            int id = userDetails.Id;

            if (id <= 0)
                return InvalidDTOFactory.InvalidUser();

            User user = unitOfWork.UserRepository.GetById(id);

            if (user == null)
                return InvalidDTOFactory.InvalidUser();

            SetUpdateProperties(userDetails, user);
            unitOfWork.UserRepository.Update(user);

            return ConverterToDTO.UserToUserDetails(user);
        }

        public void SetUpdateProperties(UserDetailsDTO userDetails, User user)
        {
            user.DateOfBirth = userDetails.DateOfBirth;
            user.DateRegistered = userDetails.DateRegistered;
            user.FirstName = userDetails.FirstName;
            user.LastName = userDetails.LastName;
        }

        public void DeleteUser(int id)
        {
            unitOfWork.UserRepository.Delete(id);
            unitOfWork.Save();
        }

        public UserDetailsDTO GetUserByUsername(string username)
        {
            var user = unitOfWork.UserRepository.GetByUsername(username);
            return ConverterToDTO.UserToUserDetails(user);
        }


    }
}
