using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services
{
    public class BookServices : IBookServices
    {
        private readonly LMSContext _context;
        private readonly ILoginAuthenticator _loginAuthenticator;

        public BookServices(LMSContext context,
                            ILoginAuthenticator loginAuthenticator)
        {
            _context = context;
            _loginAuthenticator = loginAuthenticator;
        }
        public void AddBookToDb(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public Book FindBook(string title)
        {
            CheckIfBookExist(title);
            return _context.Books.First(b => b.Title == title);
        }
        public Book FindBook(string title, string author)
        {
            CheckIfBookExist(title, author);
            return _context.Books.First(b => b.Title == title && b.Author.Name == author);
        }
        public Book FindAvailableBook(string title, string author)
        {
            var user = _loginAuthenticator.LoggedUser();
            CheckIfBookExist(title, author);
            var book = ProvideAvailableBook(title, author, user);
            SetReserveBookStatusFalse(book);
            return book;
        }
        public Book ProvideAvailableBook(string title, string author, User user)
        {
            if (!_context.Books
                      .Any(b => b.Title == title && b.Author.Name == author && b.IsCheckedOut == false))
                throw new ArgumentException($"We are sorry at this moment all copies of a book \"{title}\" are issued. You can reserve a copy, if you want.");

            if (_context.Books
                     .Any(b => b.Title == title && b.Author.Name == author
                     && b.IsCheckedOut == false && b.IsReserved == false))
                return _context.Books
                     .First(b => b.Title == title && b.Author.Name == author
                     && b.IsCheckedOut == false && b.IsReserved == false);

            if (!_context.Books
                     .Any(b => b.Title == title && b.Author.Name == author
                     && b.IsCheckedOut == false && b.IsReserved == true
                     && _context.Reservations.Any(r => r.UserId == user.Id && r.BookId == b.Id)))
                throw new ArgumentException($"We are sorry at this moment all copies of a book \"{title}\" are reserved!");

            return _context.Books
                     .First(b => b.Title == title && b.Author.Name == author
                     && b.IsCheckedOut == false && b.IsReserved == true
                     && _context.Reservations.Any(r => r.UserId == user.Id && r.BookId == b.Id));
        }
        public void SetCheckOutBookStatus(Book book)
        {
            book.IsCheckedOut = true;
        }
        public void SetReturnBookStatus(Book book)
        {
            book.IsCheckedOut = false;
        }
        public void SetReserveBookStatus(Book book)
        {
            book.IsReserved = true;
        }
        public void SetReserveBookStatusFalse(Book book)
        {
            book.IsReserved = false;
        }
        public void CheckIfBookExist(string title, string author)
        {
            if (!_context.Books.Any(b => b.Title == title && b.Author.Name == author))
                throw new ArgumentException
                        ($"Book with title \"{title}\" and author {author} does not exist!");
        }
        public bool CheckIfBookExist(string title)
        {
            return _context.Books.Any(b => b.Title == title);
        }
        public string AllBooksToString()
        {
            var allBooks = _context.Books.ToList();
            var strBulider = new StringBuilder();
            int counter = 1;
            foreach (var book in allBooks)
            {
                strBulider.AppendLine($"========Book #{counter}========{Environment.NewLine}Title : {book.Title}{Environment.NewLine}Author : {_context.Authors.First(a => a.Id == book.AuthorId).Name}{Environment.NewLine}Year : {book.Year}{Environment.NewLine}Pages : {book.Pages}{Environment.NewLine}Language : {book.Language}{Environment.NewLine}ISBN : {_context.Isbns.First(i => i.Id == book.IsbnId).ISBN}");
                counter++;
            }
            return strBulider.ToString();
        }
        public string AllBooksToString(IList<Book> books)
        {
            var allBooks = books.ToList();
            var strBulider = new StringBuilder();
            int counter = 1;
            foreach (var book in allBooks)
            {
                strBulider.AppendLine($"========Book #{counter}========{Environment.NewLine}Title : {book.Title}{Environment.NewLine}Author : {_context.Authors.First(a => a.Id == book.AuthorId).Name}{Environment.NewLine}Year : {book.Year}{Environment.NewLine}Pages : {book.Pages}{Environment.NewLine}Language : {book.Language}{Environment.NewLine}ISBN : {_context.Isbns.First(i => i.Id == book.IsbnId).ISBN}");
                counter++;
            }
            return strBulider.ToString();
        }
        public IList<Book> SearchByAuthor(string authorName)
        {
            return _context.Books.Where(b => b.Author.Name.Contains(authorName)).ToList();
        }
        public IList<Book> SearchByTitle(string title)
        {
            return _context.Books.Where(b => b.Title.Contains(title)).ToList();
        }
        public IList<Book> SearchByLanguage(string language)
        {
            return _context.Books.Where(b => b.Language.Contains(language)).ToList();
        }
        public IList<Book> SearchByYear(int year)
        {
            return _context.Books.Where(b => b.Year == year).ToList();
        }

    }
}
