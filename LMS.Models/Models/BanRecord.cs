using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.Models
{
    public class BanRecord
    {
        public BanRecord()
        {

        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
