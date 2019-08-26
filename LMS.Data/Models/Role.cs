using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Data.Models
{
    public class Role
    {
        internal Role()
        {
                
        }
        internal Role(string roleName)
        {
            this.Name = roleName;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
