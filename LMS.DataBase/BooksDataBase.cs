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
    public class BooksDataBase : IBooksDataBase
    {
        private readonly IJson _json;
        private readonly IValidator _validator;
        private readonly IGlobalMessages _messages;
        private IList<IBook> books = new List<IBook>();
        public BooksDataBase(IJson json, 
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
        public void AddBookToJsonDb(string title,string author,int pages,int year,string country,string language,string subject)
        {
            _json.AddBookToJsonDB(title, author, pages, year, country, language, subject);
        }
        public void AddBookToDb(IBook book)
        {
            books.Add(book);
        }
        public void RemoveFromDb(IBook book)
        {
            books.Remove(book);
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
    }
}