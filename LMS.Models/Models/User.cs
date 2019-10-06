using LMS.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class User : IdentityUser
    {
        public User()
        {

        }
        public ICollection<HistoryRegistry> HistoryRegistries { get; set; } = new List<HistoryRegistry>();
        public ICollection<ReserveBook> ReservedBooks { get; set; } = new List<ReserveBook>();
        public string BanRecordId { get; set; }
        public BanRecord BanRecord { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public bool IsCancelled { get; set; }

    }
}
