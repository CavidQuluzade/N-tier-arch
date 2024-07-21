using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
