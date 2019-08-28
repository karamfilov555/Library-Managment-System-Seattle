using LMS.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class User
    {
        public User()
        {
        }
        public User(string username, string password, Role role)
        {
            this.Username = username;
            this.Password = password;
            this.Role = role;
            this.HistoryRegistries = new List<HistoryRegistry>();
        }
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleId  { get; set; }
        public Role Role { get; set; }

        public int RecordFinesId { get; set; }
        public RecordFines RecordFines { get; set; }
        public ICollection<HistoryRegistry> HistoryRegistries { get; set; }
        public ICollection<ReserveBook> ReservedBooks { get; set; } = new List<ReserveBook>();
    }
}
