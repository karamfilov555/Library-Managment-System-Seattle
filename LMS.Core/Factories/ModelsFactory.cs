using LMS.Contracts;
using LMS.Generators.Contracts;
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
        private readonly IIsbnGenerator _isbnGenerator;
        public ModelsFactory(ILoginAuthenticator loginAuthenticator, IIsbnGenerator isbnGenerator)
        {
            _loginAuthenticator = loginAuthenticator;
            _isbnGenerator = isbnGenerator;
        }
        public IUser CreateUser(string username, string password)
        {
            var user = new User(username, password);
            return user;
        }
        public IBook CreateBook(string title, string author, int pages, int year, string county, string language, string subject,string isbn)
        {
            SubjectCategory _subject = (SubjectCategory)Enum.Parse(typeof(SubjectCategory), subject, true);

            var book = new Book(title, author, pages, year, county, language, _subject, isbn);
            return book;
        }
        public IHistoryRegistry CreateRegistry(string title, string author, int pages, int year, string county, string language, string subject, string isbn)
        {
            SubjectCategory _subject = (SubjectCategory)Enum.Parse(typeof(SubjectCategory),subject, true);
            var today = DateTime.Now;
            var returnDate = today.AddDays(5).ToShortDateString();
            var currentUsername = _loginAuthenticator.GetCurrentUserName();

            var registry = new HistoryRegistry(title, author, pages, year, county, language, _subject, isbn,currentUsername,returnDate);
            return registry;
        }
    }
}
