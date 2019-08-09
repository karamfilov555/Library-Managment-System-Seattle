using LMS.Contracts;
using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.Services
{
    public class BooksDataBase : IBooksDataBase
    {
        private readonly IJson _json;
        private readonly IValidator _validator;
        private IList<Book> books = new List<Book>();
        public BooksDataBase(IJson json, IValidator validator)
        {
            _json = json;
            _validator = validator;
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
        public void AddBookToDb(Book book)
        {
            books.Add(book);
        }
        public void RemoveFromDb(Book book)
        {
            books.Remove(book);
        }
        public Book FindBookInDb(string title)
        {
            var bookToFind = books.FirstOrDefault(x => x.Title == title);
            if (_validator.IsNull(bookToFind))
                throw new ArgumentException("Book with such title does not exist!");

            return bookToFind;
        }
    }
}