using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.Models
{
    public class AccountType
    {
        public AccountType()
        {

        }
        public string Id { get; set; }
        public string MembershipType { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
