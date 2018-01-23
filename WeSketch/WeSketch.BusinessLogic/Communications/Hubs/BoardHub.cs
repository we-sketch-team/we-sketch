using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using WeSketch.BusinessLogic.DTOs.BoardDTOs;

namespace WeSketch.BusinessLogic.Communications.Hubs
{
    public class BoardHub : Hub
    {
        public void SendUpdate(BoardDetailsDTO boardDetails)
        {
            
        }
    }
}
