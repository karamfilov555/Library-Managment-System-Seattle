using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

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
            this.CheckOutDate = DateTime.Now.ToShortDateString();
            this.ReturnDate = DateTime.Now.AddDays(5);
        }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public string CheckOutDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public bool IsReturned { get; set; }
    }
}
