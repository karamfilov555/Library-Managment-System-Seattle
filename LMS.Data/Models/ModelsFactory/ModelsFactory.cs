using LMS.Generators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Models.ModelsFactory
{
    public class FactoryModels : IModelsFactory
    {
        // TUK MOJE DA SE SLOJI VALIDATORA I DA SE PRAVQT PROVERKITE !
        //private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IIsbnGenerator _isbnGenerator;
        public FactoryModels(/*ILoginAuthenticator loginAuthenticator,*/
            IIsbnGenerator isbnGenerator)
        {
            //_loginAuthenticator = loginAuthenticator;
            _isbnGenerator = isbnGenerator;
        }
        public User CreateUser(string username, string password)
        {
            var user = new User(username, password);
            return user;
        }
        public Book CreateBook(string title, string authorName, int pages, int year, string country, string language, string subjectName)
        {
            // some validations here
            var author = CreateAuthor(authorName);
            var subject = CreateSubject(subjectName);
            var isbn = _isbnGenerator.GenerateISBN();
            var book = new Book(title, author, pages, year, country, language, subject, isbn);
            return book;
        }
        public HistoryRegistry CreateRegistry(User user,Book book)
        {
            var registry = new HistoryRegistry(user,book);
            return registry;
        }
        public Author CreateAuthor(string name)
        {
            var author = new Author(name);
            return author;
        }
        public SubjectCategory CreateSubject(string name)
        {
            var subject = new SubjectCategory(name);
            return subject;
        }
    }
}
