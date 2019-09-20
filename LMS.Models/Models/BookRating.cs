using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.Models
{
    public class BookRating
    {
        public BookRating()
        {

        }
        public string Id { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }

        public decimal Rating { get; set; }
    }
}
