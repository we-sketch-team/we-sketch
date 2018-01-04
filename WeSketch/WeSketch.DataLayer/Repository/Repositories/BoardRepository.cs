using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeSketch.DataLayer.Model;

namespace WeSketch.DataLayer.Repository.Repositories
{
    public class BoardRepository : IRepository<Board>
    {
        private WeSketchContext context;

        public BoardRepository(WeSketchContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            Board board = context.Boards.Find(id);
            context.Boards.Remove(board);
        }        

        public List<Board> GetAll()
        {
            return context.Boards.ToList();
        }

        public Board GetById(int id)
        {
            return context.Boards.Find(id);
        }

        public void Insert(Board typeInstance)
        {
            context.Boards.Add(typeInstance);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Board typeInstance)
        {
            context.Entry(typeInstance).State = EntityState.Modified;
        }        
    }
}
