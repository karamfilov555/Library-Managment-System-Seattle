using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class ListUsersViewModel
    {
        public ICollection<UserViewModel> Users { get; set; }
    }
}
