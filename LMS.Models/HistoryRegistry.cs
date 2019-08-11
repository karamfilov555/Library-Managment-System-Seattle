using LMS.Models.Enums;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models
{
    public class HistoryRegistry : IHistoryRegistry
    {
        private string title;
        private string username;
        private string isbn;
        private string returnDate;
        private string reservation;
        private string author;
        private string language;
        private SubjectCategory subject;
        private string country;
        private int pages;
        private int year;

        public HistoryRegistry(string title, string author, int pages, int year, string country, string language, SubjectCategory subject, string ISBN, string username,string returnDate)
        {
            this.Title = title;
            this.Author = author;
            this.Pages = pages;
            this.Year = year;
            this.Country = country;
            this.Language = language;
            this.Subject = subject;
            this.ISBN = ISBN;
            this.Username = username;
            this.ReturnDate = returnDate;
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string ISBN
        {
            get
            {
                return isbn;
            }
            set
            {
                isbn = value;
            }
        }
        public string ReturnDate
        {
            get
            {
                return returnDate;
            }
            set
            {
                returnDate = value;
            }
        }
        public string Author { get; private set; }
        public int Pages { get; private set; }
        public int Year { get; private set; }
        public string Country { get; private set; }
        public string Language { get; private set; }
        public SubjectCategory Subject { get; private set; }
        public string RegistryInfo()
        {
            string spaces = new string(' ', 4);
            return $"{Environment.NewLine}" +
                   $"{spaces}Title: {this.Title}{Environment.NewLine}" +
                   $"{spaces}ISBN: {this.ISBN}{Environment.NewLine}" +
                   $"{spaces}ReturnDate: {this.ReturnDate}{Environment.NewLine}" +
                   $"======================================================";
        }
    }
}
