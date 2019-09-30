using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Web.Models
{
    public class NotificationViewModel
    {
        [Required]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public DateTime EventDate{ get; set; }
        [Required]
        public string Username{ get; set; }
        [Required]
        public bool IsSeen { get; set; }
    }
}
