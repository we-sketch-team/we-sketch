using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.DataLayer.Model;

namespace WeSketch.DataLayer.Repository.Repositories
{
    public class ChatRoomRepository : IRepository<ChatRoom>
    {
        private WeSketchContext context;

        public ChatRoomRepository(WeSketchContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            ChatRoom chat = context.ChatRooms.Find(id);
            context.ChatRooms.Remove(chat);
        }      

        public List<ChatRoom> GetAll()
        {
            return context.ChatRooms.ToList();
        }

        public ChatRoom GetById(int id)
        {
            return context.ChatRooms.Find(id);
        }

        public void Insert(ChatRoom typeInstance)
        {
            context.ChatRooms.Add(typeInstance);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(ChatRoom typeInstance)
        {
            context.Entry(typeInstance).State = EntityState.Modified;
        }        
    }
}
