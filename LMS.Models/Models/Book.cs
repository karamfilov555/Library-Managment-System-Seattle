using LMS.Models.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Book
    {
        public Book()
        {

        }
        public string Id { get; set; }
        //validations!!!!!!!
        [Required]
        public string Title { get; set; }

        public string AuthorId { get; set; }
        [Required]
        public Author Author { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public int Copies { get; set; }
        public bool IsReserved { get; set; }
        public bool IsCheckedOut { get; set; }
        public ICollection<BookSubject> BookSubject { get; set; }
        public ICollection<ReserveBook> ReservedBooks { get; set; } 
        public ICollection<HistoryRegistry> HistoryRegistries { get; set; }
        public string BookRatingId { get; set; }
        public BookRating BookRating { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
