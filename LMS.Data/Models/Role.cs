using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Data.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
