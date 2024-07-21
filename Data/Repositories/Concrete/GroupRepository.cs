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
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        private readonly CourseAppDbContext _context;
        public GroupRepository(CourseAppDbContext context) : base(context)
        {
               _context = context;
        }

        public Group GetGroupByName(string name)
        {
            return _context.Groups.FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
        }
        public Group GetStudentOfGroupById(int id)
        {
            return _context.Groups.Include(x => x.Students).FirstOrDefault(x => x.Id == id);
        }
        public void GetAllGroups()
        {
            foreach (var group in _context.Groups)
            {
                Console.WriteLine($"Id: {group.Id} | Name: {group.Name} | Limit: {group.Limit} | Begin date: {group.BeginDate} | End date: {group.EndDate}");
            }
        }

        public int GetAllGroupsCount()
        {
            return _context.Groups.Count();
        }
    }
}
