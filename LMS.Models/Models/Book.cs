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
        public Book(string title, Author author, int pages, int year, string country, string language, 
            Isbn isbn)
        {
            this.Title = title;
            this.Author = author;
            this.Pages = pages;
            this.Year = year;
            this.Country = country;
            this.Language = language;
            this.Isbn = isbn;
            this.BookSubject = new List<BookSubject>();
            this.ReservedBooks = new List<ReserveBook>();
            this.HistoryRegistries = new List<HistoryRegistry>();
        }
        public string Id { get; set; }

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
        public string IsbnId { get; set; }
        [Required]
        public Isbn Isbn{ get; set; }
        public bool IsReserved { get; set; }
        public bool IsCheckedOut { get; set; }
        public ICollection<BookSubject> BookSubject { get; set; }
        public ICollection<ReserveBook> ReservedBooks { get; set; } 
        public ICollection<HistoryRegistry> HistoryRegistries { get; set; }
        public string BookRatingId { get; set; }
        public BookRating BookRating { get; set; }

    }
}
