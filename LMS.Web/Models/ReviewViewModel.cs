using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class ReviewViewModel
    {
        public string Description { get; set; }
        public decimal Grade { get; set; }
        public string BookId { get; set; }
        public string UserId { get; set; }
    }
}
