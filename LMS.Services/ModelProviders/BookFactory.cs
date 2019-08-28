using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

using System.Collections.Generic;
using System.Linq;

namespace LMS.Services.ModelProviders
{
    public class BookFactory : IBookFactory
    {
        private readonly IAuthorServices _authorServices;
        private readonly ISubjectServices _subjectServices;
        private readonly IIsbnServices _isbnServices;

        public BookFactory(IIsbnServices isbnServices,
                           IAuthorServices authorServices,
                           ISubjectServices subjectServices)
        {
            _authorServices = authorServices;
            _subjectServices = subjectServices;
            _isbnServices = isbnServices;
        }
        public Book CreateBook(string title, string authorName, int pages, int year, string country, string language, string[] subjects)
        {
            var author = _authorServices.ProvideAuthor(authorName);
            var subject = _subjectServices.ProvideSubject(subjects);
            var isbn = _isbnServices.ProvideIsbn();
            var book = new Book(title, author, pages, year, country, language, isbn);
            book.BookSubject = subject;
            return book;
        }
    }
}
