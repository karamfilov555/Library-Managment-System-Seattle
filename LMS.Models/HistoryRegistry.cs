using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models
{
    public class HistoryRegistry : IHistoryRegistry
    {
        private string title;
        private string author;
        private int pages;
        private int year;
        private string username;
        private string isbn;
        private string returnDate;
        private string country;
        private string language;
        private string subject;
     
        public HistoryRegistry(string title, string author, int pages, int year, string country, string language, string subject,string username, string ISBN,string returnDate)
        {
            this.Title = title;
            this.ISBN = ISBN;
            this.Author = author;
            this.Pages = pages;
            this.Year = year;
            this.Country = country;
            this.Language = language;
            this.Subject = subject;
            this.Username = username;
            this.ReturnDate = returnDate;
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public string Language
        {
            get { return language; }
            set { language = value; }
        }
        public int Pages
        {
            get { return pages; }
            set { pages = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }
        public string ISBN
        {
            get { return isbn; }
            set { isbn = value; }
        }
        public string ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }
        public string BookArchive()
        {
            string spaces = new string(' ', 13);
            return $"{Environment.NewLine}" +
                   $"{spaces}Title: {this.Title}{Environment.NewLine}" +
                   $"{spaces}ReturnDate: {this.ReturnDate}{Environment.NewLine}" +
                   $"{spaces}ISBN: {this.ISBN}{Environment.NewLine}";
        }
    }
}
