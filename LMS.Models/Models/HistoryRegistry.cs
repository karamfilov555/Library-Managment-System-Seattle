using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class HistoryRegistry
    {
        public HistoryRegistry()
        { }
        public HistoryRegistry(User user, Book book)
        {
            this.User = user;
            this.Book = book;
        }
        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
