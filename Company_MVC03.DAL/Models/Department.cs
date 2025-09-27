using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_MVC03.DAL.Models
{
    public class Department : BaseEntity
    {
        // V05
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }


    }
}
