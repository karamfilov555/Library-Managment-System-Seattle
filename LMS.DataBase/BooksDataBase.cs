using LMS.Contracts;
using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;

namespace LMS.DataBase
{
    public class BooksDataBase : IBooksDataBase
    {
        private readonly IJson _json;
        private IList<Book> books = new List<Book>();
        public BooksDataBase(IJson json)
        {
            this._json = json;
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
    }
}