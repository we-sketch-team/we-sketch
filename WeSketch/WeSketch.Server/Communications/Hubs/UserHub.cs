using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.BusinessLogic.DTOs;
using WeSketch.BusinessLogic.Services;

namespace WeSketch.Server.Communications.Hubs
{
    public class UserHub : Hub
    {
        private DataService dataService = BusinessLogic.Utilities.ObjectFactory.GetDataService();
        
        public UserDetailsDTO Login(LoginDTO loginDTO)
        {
            var message = $"Login from: {loginDTO.Email}";
            Logger.Log(message);
            return dataService.Login(loginDTO);
        }

        public UserDetailsDTO CreateAccount(CreateUserDTO createUserDTO)
        {
			Logger.Log($"Creating account..");
			UserDetailsDTO user = dataService.CreateAccount(createUserDTO);
			Logger.Log($"Account created. UserId: {user.Id}");
			return user;
        }

        public UserDetailsDTO GetUser(int userId)
        {
			Logger.Log($"Getting user information for user with id: {userId}");
			return dataService.GetUser(userId);
        }

        public UserDetailsDTO UpdateUser(UserDetailsDTO userDetailsDTO)
        {
			Logger.Log($"Updating user information for user with id: {userDetailsDTO.Id}");
			return dataService.UpdateUser(userDetailsDTO);
        }

        public UserDetailsDTO GetUserByUsername(string username)
        {
			Logger.Log($"Getting user information for user with username: {username}");
			return dataService.GetUserByUsername(username);
        }

        public void DeleteUser(int userId)
        {
            dataService.DeleteUser(userId);
			Logger.Log($"Deleted usr with id: {userId}");
		}
	}
}
