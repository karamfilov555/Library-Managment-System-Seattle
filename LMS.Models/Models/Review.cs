using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.Models
{
    public class Review
    {
        public Review()
        {

        }
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal Grade { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
