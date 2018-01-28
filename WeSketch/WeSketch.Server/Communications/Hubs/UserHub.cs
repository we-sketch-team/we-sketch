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
            return dataService.CreateAccount(createUserDTO);
        }

        public UserDetailsDTO GetUser(int userId)
        {
            return dataService.GetUser(userId);
        }

        public UserDetailsDTO UpdateUser(UserDetailsDTO userDetailsDTO)
        {
            return dataService.UpdateUser(userDetailsDTO);
        }

        public void DeleteUser(int userId)
        {
            dataService.DeleteUser(userId);
        }
    }
}
