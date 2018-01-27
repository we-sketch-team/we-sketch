using System;
using System.Collections.Generic;
using WeSketch.BusinessLogic.Utilities;
using WeSketch.DataLayer.Model;
using WeSketch.DataLayer.UnitOfWork;
using WeSketch.BusinessLogic.DTOs.ChatRoomDTOs;
using WeSketch.BusinessLogic.DTOs.MessageDTOs;

namespace WeSketch.BusinessLogic.Providers
{
    public class ChatRoomProvider : IDisposable
    {
        private Mediator mediator;
        private UnitOfWork unitOfWork;       

        public ChatRoomProvider(Mediator mediator)
        {
            this.mediator = mediator;     
            unitOfWork = ObjectFactory.GetUnitOfWork();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public void SetMediatorChatRoom(int id)
        {
            ChatRoom chatRoom = unitOfWork.ChatRoomRepository.GetById(id);
            this.mediator.ChatRoom = chatRoom;
        }

        public List<ChatRoomDetailsDTO> GetAllChatRooms()
        {
            List<ChatRoom> chatRooms = unitOfWork.ChatRoomRepository.GetAll();
            List<ChatRoomDetailsDTO> result = new List<ChatRoomDetailsDTO>();

            foreach (var chatRoom in chatRooms)
            {
                result.Add(ConverterToDTO.ChatToChatDetails(chatRoom));
            }

            return result;
        }

        public ChatRoomDetailsDTO GetChatRoom(int id)
        {
            ChatRoom chatRoom = unitOfWork.ChatRoomRepository.GetById(id);
            return ConverterToDTO.ChatToChatDetails(chatRoom);
        }

        public void CreateChatRoom()
        {
            ChatRoom chatRoom = new ChatRoom();
            chatRoom.ActiveChat = true;
            chatRoom.DateCreated = DateTime.Now;

            unitOfWork.ChatRoomRepository.Insert(chatRoom);
            unitOfWork.Save();

            mediator.ChatRoom = chatRoom;
        }

        public void DeleteChatRoom(int id)
        {
            unitOfWork.ChatRoomRepository.Delete(id);
            unitOfWork.Save();
        }

        public ChatRoomDetailsDTO UpdateChatRoom(UpdateChatRoomDTO updateChatRoomDTO)
        {
            ChatRoom chatRoom = unitOfWork.ChatRoomRepository.GetById(updateChatRoomDTO.Id);
            chatRoom.ActiveChat = updateChatRoomDTO.ActiveChat;

            unitOfWork.ChatRoomRepository.Update(chatRoom);
            unitOfWork.Save();

            return ConverterToDTO.ChatToChatDetails(chatRoom);
        }

        public List<MessageDetailsDTO> GetAllMessages()
        {
            List<Message> messages = unitOfWork.MessageRepository.GetAll();
            List<MessageDetailsDTO> result = new List<MessageDetailsDTO>();

            foreach (var message in messages)
            {
                result.Add(ConverterToDTO.MessageToMessageDetails(message));
            }

            return result;
        }

        public MessageDetailsDTO GetMessage(int id)
        {
            Message message = unitOfWork.MessageRepository.GetById(id);
            return ConverterToDTO.MessageToMessageDetails(message);
        }

        public MessageDetailsDTO CreateMessage(CreateMessageDTO createMessage)
        {
            Message message = new Message();
            message.Content = createMessage.Content;

            message.SentIn = unitOfWork.ChatRoomRepository.GetById(createMessage.ChatRoomId);
            message.Sender = mediator.User;

            message.ChatRoomId = createMessage.ChatRoomId;
            message.UserID = mediator.User.Id;

            unitOfWork.MessageRepository.Insert(message);
            unitOfWork.Save();

            return ConverterToDTO.MessageToMessageDetails(message);
        }

        public void DeleteMessage(int id)
        {
            unitOfWork.MessageRepository.Delete(id);
            unitOfWork.Save();
        }
    }
}
