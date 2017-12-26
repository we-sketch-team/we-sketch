using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeSketch.DataLayer.Model;
using WeSketch.DataLayer.UnitOfWork;
using WeSketch.BusinessLogic.Utilities;
using WeSketch.BusinessLogic.Services;
using WeSketch.BusinessLogic.DTOs.ChatRoomDTOs;
using WeSketch.BusinessLogic.DTOs.MessageDTOs;

namespace WeSketch.BackEndTests.Controllers
{
    [RoutePrefix("api/chatrooms")]
    public class ChatRoomsController : ApiController
    {
        #region ChatRoom CRUD
        [Route("chats/crud/getall")]
        [HttpGet]
        public List<ChatRoomDetailsDTO> GetAllChatRooms()
        {
           DataService dataService = ObjectFactory.GetDataService();
           return dataService.GetAllChatRoom();
        }

        [HttpGet]
        [Route("chats/crud/byid/{id:int}")]
        public ChatRoomDetailsDTO GetChatRoomById(int id)
        {
            DataService dataService = ObjectFactory.GetDataService();
            return dataService.GetChatRoom(id);
        }

        [HttpPost]
        [Route("chats/crud/insert")]
        public ChatRoomDetailsDTO CreateChatRoom()
        {
            DataService dataService = ObjectFactory.GetDataService();
            return dataService.CreateChatRoom();
        }

        [HttpPut]
        [Route("chats/crud/update")]
        public ChatRoomDetailsDTO UpdateChatRoom([FromBody] UpdateChatRoomDTO chatRoom)
        {
            DataService dataService = ObjectFactory.GetDataService();
            return dataService.UpdateChatRoom(chatRoom);
        }

        [HttpDelete]
        [Route("chats/crud/delete/{id:int}")]
        public void DeleteChatRoom(int id)
        {
            DataService dataService = ObjectFactory.GetDataService();
            dataService.DeleteChatRoom(id);
        }
        #endregion
        #region Messages CRUD
        [Route("messages/crud")]
        [HttpGet]
        public List<MessageDetailsDTO> GetAllMessages()
        {
            DataService dataService = ObjectFactory.GetDataService();
            return dataService.GetAllMessages();
        }

        [HttpGet]
        [Route("messages/crud/byid/{id:int}")]
        public MessageDetailsDTO GetMessageById(int id)
        {
            DataService dataService = ObjectFactory.GetDataService();
            return dataService.GetMessage(id);
        }

        [HttpPost]
        [Route("messages/crud/insert")]
        public MessageDetailsDTO InsertMessage([FromBody] CreateMessageDTO message)
        {
            DataService dataService = ObjectFactory.GetDataService();
            return dataService.CreateMessage(message);
        }        

        [HttpDelete]
        [Route("messages/crud/delete/{id:int}")]
        public void DeleteMessage(int id)
        {
            DataService dataService = ObjectFactory.GetDataService();
            dataService.DeleteMessage(id);
        }
        #endregion

    }
}
