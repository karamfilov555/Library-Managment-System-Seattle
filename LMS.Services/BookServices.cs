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
            if(!CheckIfBookExist(title,author))
                throw new ArgumentException("The book does not exist!");
            return _context.Books.First(t => t.Title == title && t.Author.Name == author);
        }
        public void SetReserveBookStatus(Book book)
        {
            book.IsReserved = true;
        }

        public bool CheckIfBookExist(string title,string author)
        {
            return _context.Books.Any(b => b.Title == title && b.Author.Name == author);
        }
        public bool CheckIfBookExist(string title)
        {
            return _context.Books.Any(b => b.Title == title);
        }
    }
}
