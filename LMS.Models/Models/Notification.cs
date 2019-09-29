using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models
{
    public class Notification
    {
        public Notification()
        {

        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}
