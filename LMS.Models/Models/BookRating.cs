using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; }
    }
}
