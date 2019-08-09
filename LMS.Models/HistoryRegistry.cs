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
     
        public HistoryRegistry(string title,string ISBN, string username,string returnDate)
        {
            this.Title = title;
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
            private set
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
            private set
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
            private set
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
            private set
            {
                returnDate = value;
            }
        }
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
