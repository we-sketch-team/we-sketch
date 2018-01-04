using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.DataLayer.Model;

namespace WeSketch.DataLayer.Repository.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private WeSketchContext context;

        public UserRepository(WeSketchContext context)
        {
            this.context = context;                
        }

        public void Delete(int id)
        {
            User user = context.Users.Find(id);
            context.Users.Remove(user);
        }        

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(int id)
        {
            return context.Users.Find(id);
        }

        public void Insert(User typeInstance)
        {
            context.Users.Add(typeInstance);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(User typeInstance)
        {
            context.Entry(typeInstance).State = EntityState.Modified;
        }

        public User GetByUsername(string username)
        {
            return context.Users.FirstOrDefault<User>(s => s.Username == username);
        }

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault<User>(s => s.Email == email);
        }        
    }
}
