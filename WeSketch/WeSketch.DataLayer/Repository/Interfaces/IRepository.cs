using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSketch.DataLayer.Repository
{
    public interface IRepository<Type>
    {
        List<Type> GetAll();
        Type GetById(int id);
        void Insert(Type typeInstance);
        void Delete(int id);
        void Update(Type typeInstance);
        void Save();
    }
}
