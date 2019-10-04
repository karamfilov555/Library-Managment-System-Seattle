using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LMS.Models.Models
{
    public class Review
    {
        public Review()
        {

        }
        public string Id { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Grade { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string BookRatingId { get; set; }
        public BookRating BookRating { get; set; }
        public string BookTitle { get; set; }
    }
}
