using Core.Entities.Base;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base_Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly CourseAppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(CourseAppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
        }
    }
}
