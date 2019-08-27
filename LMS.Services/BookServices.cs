using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using System;

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
    }
}
