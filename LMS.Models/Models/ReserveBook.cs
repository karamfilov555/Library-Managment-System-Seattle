using Microsoft.AspNetCore.Identity;
using System;

namespace LMS.Models
{
    public class ReserveBook
    {
        public ReserveBook()
        {

        }

        public ReserveBook(User user,Book book)
        {
            this.User = user;
            this.Book = book;
            this.ReservationDate = DateTime.Now.ToShortDateString();
        }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public string ReservationDate { get; set; }
    }
}
