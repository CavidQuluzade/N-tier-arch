using Core.Entities;
using Data.Repositories.Base_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Abstract
{
    internal interface IGroupRepository : IBaseRepository<Group>
    {
        public Group GetGroupByName(string name);
        public Group GetStudentOfGroupById(int id);
        public void GetAllGroups();
        public int GetAllGroupsCount();

    }
}
