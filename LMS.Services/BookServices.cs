using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using System;
using System.Linq;

namespace LMS.Services
{
    public class BookServices : IBookServices
    {
        private readonly LMSContext _context;

        public BookServices(LMSContext context)
        {
            _context = context;
        }
        public void AddBookToDb(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public Book FindBook(string title, string author)
        {
            CheckIfBookExist(title, author);
            return _context.Books.First(b => b.Title == title && b.Author.Name == author);
        }
        public Book FindAvailableBook(string title, string author)
        {
            CheckIfBookExist(title, author);
            CheckIfBookIsAvailable(title, author);
            return _context.Books.First(b => b.Title == title && b.Author.Name == author);
        }
        public void CheckIfBookIsAvailable(string title, string author)
        {
            if (!_context.Books.Any(b => b.Title == title && b.Author.Name == author 
                                        && b.IsCheckedOut == false))
                throw new ArgumentException($"We are sorry at this moment all copies of a book \"{title}\" are issued. You can reserve a copy, if you want.");
        }
        public void SetReserveBookStatus(Book book)
        {
            book.IsReserved = true;
        }
        public void SetCheckOutBookStatus(Book book)
        {
            book.IsCheckedOut = true;
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
    }
}
