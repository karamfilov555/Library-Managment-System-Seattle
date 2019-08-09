using LMS.Contracts;
using LMS.Models;
using LMS.Models.Enums;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        private readonly ILoginAuthenticator _loginAuthenticator;
        public ModelsFactory(ILoginAuthenticator loginAuthenticator)
        {
            _loginAuthenticator = loginAuthenticator;
        }
        public IUser CreateUser(string username, string password)
        {
            var user = new User(username, password);
            return user;
        }
        public IBook CreateBook(string title, string author, int pages, int year, string county, string language, string subject)
        {
            SubjectCategory subj = (SubjectCategory)Enum.Parse(typeof(SubjectCategory), subject, true);
            var book = new Book(title, author, pages, year, county, language, subj,"isnm");
            return book;
        }
        public IHistoryRegistry CreateRegistry(string title)
        {
            var today = DateTime.Now;
            var returnDate = today.AddDays(5).ToShortDateString();
            var currentUsername = _loginAuthenticator.GetCurrentUserName();
            var isbn = "isbn";
            var registry = new HistoryRegistry(title,isbn,currentUsername,returnDate);
            return registry;
        }
    }
}
