using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base_Repository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
