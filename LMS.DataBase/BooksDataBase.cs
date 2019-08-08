using LMS.Contracts;
using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LMS.DataBase
{
    public class BooksDataBase : IBooksDataBase
    {
        private readonly IJson _json;
        private readonly IValidator _validator;
        private IList<Book> books = new List<Book>();
        public BooksDataBase(IJson json, IValidator validator)
        {
            this._json = json;
            this._validator = validator;
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
            this._json.AddBookToJsonDB(title, author, pages, year, country, language, subject);
        }
        public void AddBookToDb(Book book)
        {
            this.books.Add(book);
        }
        public void RemoveFromDb(Book book)
        {
            this.books.Remove(book);
        }
        public Book FindBookInDb(string title)
        {
            var bookToFind = this.books.FirstOrDefault(x => x.Title == title);
            if (this._validator.IsNull(bookToFind))
                throw new ArgumentException("Book with such title does not exist!");

            return bookToFind;
        }
    }
}