using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_MVC03.DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public bool IsAgree { get; set; }

    }
}
