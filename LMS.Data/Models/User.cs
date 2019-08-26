using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Data.Models
{
    public class User
    {
        internal User()
        {
        }
        internal User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public ICollection<HistoryRegistry> BooksOfCurrentUser { get; set; }

        //[Required]
        //public string AccessLevel { get; set; }
    }
}
