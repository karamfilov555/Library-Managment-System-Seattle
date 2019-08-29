using System;

namespace LMS.Models.Models
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
        }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime? ReservationDate { get; set; }
    }
}
