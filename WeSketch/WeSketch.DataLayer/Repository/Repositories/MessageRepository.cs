using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.DataLayer.Model;

namespace WeSketch.DataLayer.Repository.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private WeSketchContext context;

        public MessageRepository(WeSketchContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Message message = context.Messages.Find(id);
            context.Messages.Remove(message);
        }       

        public List<Message> GetAll()
        {
            return context.Messages.ToList();
        }

        public Message GetById(int id)
        {
            return context.Messages.Find(id);
        }

        public void Insert(Message typeInstance)
        {
            context.Messages.Add(typeInstance);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Message typeInstance)
        {
            context.Entry(typeInstance).State = EntityState.Modified;
        }        
    }
}
