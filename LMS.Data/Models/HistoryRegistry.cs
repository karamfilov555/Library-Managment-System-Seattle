using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Data.Models
{
    public class HistoryRegistry
    {
        internal HistoryRegistry()
        { }
        internal HistoryRegistry(User user, Book book)
        {
            this.User = user;
            this.Book = book;
        }
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Book Book { get; set; }
    }
}
