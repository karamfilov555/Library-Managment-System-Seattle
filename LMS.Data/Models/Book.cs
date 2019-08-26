using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Data.Models
{
    public class Book
    {
        internal Book()
        {

        }
        internal Book(string title, Author author, int pages, int year, string country, string language, SubjectCategory subject, string isbn)
        {
            this.Title = title;
            this.Author = author;
            this.Pages = pages;
            this.Year = year;
            this.Country = country;
            this.Language = language;
            this.SubjectCategory = subject;
            this.ISBN = isbn;
        }
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public SubjectCategory SubjectCategory { get; set; }
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
        public string ISBN { get; set; }

        //public char Rack

        //public int Copies

        //public string Reservation
    }
}
