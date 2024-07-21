using Core.Entities;
using Data.Context;
using Data.Repositories.Abstract;
using Data.Repositories.Base_Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly CourseAppDbContext _context;
        
        public StudentRepository(CourseAppDbContext context) : base(context)
        {
            _context = context;
        }
        public Student GetByIdWithGroup(int id)
        {
            return _context.Students.Include(x => x.Group).FirstOrDefault(x => x.Id == id);
        }
        public int GetAllStudentsCount()
        {
            return _context.Students.Count();
        }
        public void GetAllStudents()
        {
            foreach (var student in _context.Students)
            {
                Console.WriteLine($"Id: {student.Id} | Name: {student.Name} | Surname: {student.Surname}");
            }
        }
    }
}
