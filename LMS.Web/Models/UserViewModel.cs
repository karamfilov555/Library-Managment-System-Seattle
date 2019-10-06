using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

     //some validations
        public string Username { get; set; }

        public string RoleId { get; set; }
        public string Role { get; set; }

    }
}
