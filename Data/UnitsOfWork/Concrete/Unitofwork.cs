using Core.Messages;
using Data.Context;
using Data.Repositories.Concrete;
using Data.UnitsOfWork.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitsOfWork.Concrete
{
    public class Unitofwork : IUnitOfWork
    {
        public readonly GroupRepository Groups;
        public readonly StudentRepository Students;
        private readonly CourseAppDbContext _context;
        public Unitofwork()
        {
            _context = new CourseAppDbContext();
            Groups = new GroupRepository(_context);
            Students = new StudentRepository(_context);
        }
        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                ErrorMessages.ErrorMessage();
            }
        }
    }
}
