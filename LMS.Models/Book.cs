using LMS.Models.Enums;
using LMS.Models.ModelsContracts;
using System;
using System.Linq;

namespace LMS.Models
{
    public class Book :  IBook
    {
        private int copies;
        private string isbn;
        private string reservation;
        private string title;
        private string author;
        private string language;
        private SubjectCategory subject;
        private string country;
        private int pages;
        private int year;
        private char  rack;

        public Book(string title, string author, int pages, int year, string country, string language, SubjectCategory subject, string ISBN)
        {
            this.Title = title;
            this.Author = author;
            this.Pages = pages;
            this.Year = year;
            this.Country = country;
            this.Language = language;
            this.Subject = subject;
            this.ISBN = ISBN;
        }
        public string Title
        {
            get
            {
                return title;
            }
            private set
            {
                if (string.IsNullOrEmpty(value)||
                    value.Length < 2 || value.Length > 100)
                    throw new ArgumentException("Book's title must be between 2 and 100 symbols!");
                title = value.Replace('_', ' ');
            }
        }
        public SubjectCategory Subject
        {
            get
            {
                return subject;
            }
            private set
            {
                subject = value;
            }
        }
        public  string Author
        {
            get
            {
                return author;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) || 
                    value.Length < 3 || value.Length > 50)
                    throw new ArgumentException("Book's author name must be between 3 and 50 symbols!");
                author = value.Replace('_', ' ');
            }
        }
        public  int Pages
        {
            get
            {
                return pages;
            }
            private set
            {
                if (value < 1 || value > 100000)
                    throw new ArgumentException("Book's pages must be between 1 and 100000!");
                pages = value;
            }
        }
        public  int Year
        {
            get
            {
                return year;
            }
            private set
            {
                if (value > (int)DateTime.Now.Year)
                    throw new ArgumentException("Publication year can be greater than present year!");
                if (value < -10000)
                    throw new ArgumentException("It has been a long long time ago...be more modern");
                year = value;
            }
        }
        public  string Country
        {
            get
            {
                return country;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) ||
                    value.Length < 3 || value.Length > 50)
                    throw new ArgumentException("Country name must be between 3 and 50 symbols!");
                country = value;
            }
        }
        public  string Language
        {
            get
            {
                return language;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) || 
                    value.Length < 3 || value.Length > 50)
                    throw new ArgumentException("Language name must be between 3 and 50 symbols!");
                language = value;
            }
        }
        public char Rack
        {
            get
            {
                return rack;
            }
            private set
            {
                rack = Title[0];
            }
        }
        public int Copies
        {
            get
            {
                return copies;
            }
            set
            {
                copies = value;
            }
        }
        public string ISBN
        {
            get
            {
                return isbn;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("ISBN cannot be null!");
                isbn = value;
            }
        }
        public string Reservation
        {
            get
            {
                return reservation;
            }
            set
            {
                reservation = value;
            }
        }
        public string PrintBookInfo()
        {
            var timeOfAdding = DateTime.Now.ToLongDateString();

            return  
                $"Title: {Title}{Environment.NewLine}" +
                $"Author: {Author}{Environment.NewLine}" +
                $"Subject: {Subject}{Environment.NewLine}" +
                $"Pages: {Pages}{Environment.NewLine}" +
                $"Year: {Year}{Environment.NewLine}" +
                $"Country: {Country}{Environment.NewLine}" +
                $"Language: {Language}{Environment.NewLine}"+
                $"Reservation: {Reservation}{Environment.NewLine}" +
                $"ISBN: {ISBN}{Environment.NewLine}" +
                //$"Copies available: {CopiesCount.GetCopiesCount(key)}{Environment.NewLine}" +
                $"Added on: {timeOfAdding}{Environment.NewLine}";
        }
    }
}
