using LMS.Contracts;
using LMS.JasonDB.Contracts;
using LMS.Models;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using LMS.Models.ModelsContracts;

namespace LMS.Services
{
    public class BookServices : IBookServices
    {
        private readonly IJson _json;
        private readonly IValidator _validator;
        private readonly IGlobalMessages _messages;
        private IList<IBook> books = new List<IBook>();
        public BookServices(IJson json, 
                            IValidator validator,
                            IGlobalMessages messages)
        {
            _json = json;
            _validator = validator;
            _messages = messages;
        }
        public void LoadBooksFromJson()
        {
            var booksFromJson = _json.ReadBooks();
            foreach (var book in booksFromJson)
            {
                 books.Add(book);
            }
        }
        public void AddBookToDb(IBook book)
        {
            books.Add(book);
            var subj = book.Subject.ToString();
            var isbn = book.ISBN;
            _json.AddBookToJsonDB(book.Title, book.Author, book.Pages, book.Year, book.Country, book.Language, subj, isbn);
        }
        public void RemoveFromDb(IBook book)
        {
            books.Remove(book);
            _json.RemoveBookFromJsonDb(book.Title);
        }
        public IBook FindBookInDb(string title)
        {
            var bookToFind = books.FirstOrDefault(x => x.Title == title);
            if (_validator.IsNull(bookToFind))
                throw new ArgumentException("Book with such title does not exist!");
            return bookToFind;
        }
        public string AllExistingBooksToString()
        {
            var strBuilder = new StringBuilder();
            var count = 1;
            foreach (var book in books)
            {
                strBuilder.AppendLine(_messages.CatalogDelimiter(count) + Environment.NewLine + book.PrintBookInfo());
                count++;
            }
            return strBuilder.ToString();
        }
        public string GiveAllBooksWithThisTitle(string title)
        {
            var booksWithThisTitle = books.Where(x => x.Title == title);
            var strBuilder = new StringBuilder();
            foreach (var book in booksWithThisTitle)
            {
                strBuilder.AppendLine("ISBN: " + book.ISBN);
            }
            return strBuilder.ToString();
        }
        public string ShowAllBooksWithThisTitle(string title)
        {
            var booksWithThisTitle = books.Where(x => x.Title.ToLower().Contains(title));
            var strBuilder = new StringBuilder();
            int count = 1;
            foreach (var book in booksWithThisTitle)
            {
                strBuilder.AppendLine(_messages.CatalogDelimiter(count) + Environment.NewLine + book.PrintBookInfo());
                count++;
            }
            if (strBuilder.Length == 0)
                throw new ArgumentException("There are no books with this title!");
            return strBuilder.ToString();
        }
        public string ShowAllBooksWithThisAuthor(string author)
        {
            var booksWithThisAuthor = books.Where(x => x.Author.ToLower().Contains(author));
            var strBuilder = new StringBuilder();
            int count = 1;
            foreach (var book in booksWithThisAuthor)
            {
                strBuilder.AppendLine(_messages.CatalogDelimiter(count) + Environment.NewLine + book.PrintBookInfo());
                count++;
            }
            if (strBuilder.Length == 0)
                throw new ArgumentException("There are no books with this auhtor!");
            return strBuilder.ToString();
        }
        public string ShowAllBooksWithThisLanguage(string language)
        {
            var booksWithThisLanguage = books.Where(x => x.Language.ToLower().Contains(language));
            var strBuilder = new StringBuilder();
            int count = 1;
            foreach (var book in booksWithThisLanguage)
            {
                strBuilder.AppendLine(_messages.CatalogDelimiter(count) + Environment.NewLine + book.PrintBookInfo());
                count++;
            }
            if (strBuilder.Length == 0)
                throw new ArgumentException("There are no books with this language!");
            return strBuilder.ToString();
        }
        public string ShowAllBooksWithThisSubject(string subject)
        {
            var booksWithThisSubject = books.Where(x => x.Subject.ToString().ToLower().Contains(subject));
            var strBuilder = new StringBuilder();
            int count = 1;
            foreach (var book in booksWithThisSubject)
            {
                strBuilder.AppendLine(_messages.CatalogDelimiter(count) + Environment.NewLine + book.PrintBookInfo());
                count++;
            }
            if (strBuilder.Length == 0)
                throw new ArgumentException("There are no books with this subject!");
            return strBuilder.ToString();
        }
        public string ShowAllBooksWithThisYear(int year)
        {
            var booksFromThisYear = books.Where(x => x.Year == year);
            var strBuilder = new StringBuilder();
            int count = 1;
            foreach (var book in booksFromThisYear)
            {
                strBuilder.AppendLine(_messages.CatalogDelimiter(count) + Environment.NewLine + book.PrintBookInfo());
                count++;
            }
            if (strBuilder.Length == 0)
                throw new ArgumentException("There are no books with this publication year!");
            return strBuilder.ToString();
        }
    }
}