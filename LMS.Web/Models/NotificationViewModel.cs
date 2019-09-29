using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class NotificationViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public DateTime EventDate{ get; set; }
        public string Username{ get; set; }
    }
}
