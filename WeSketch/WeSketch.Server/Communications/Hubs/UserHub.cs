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
            return dataService.Login(loginDTO);
        }

        public UserDetailsDTO CreateAccount(CreateUserDTO createUserDTO)
        {
            return dataService.CreateAccount(createUserDTO);
        }
    }
}
