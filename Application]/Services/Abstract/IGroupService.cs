using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_.Services.Abstract
{
    public interface IGroupService
    {
        void AddGroup();
        void RemoveGroup();
        void UpdateGroup();
        void GetDetailsOfGroup();
        void GetAllGroups();
    }
}
