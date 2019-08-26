using LMS.Data;
using LMS.Data.Models;
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
            _context.Book.Add(book);
            _context.SaveChanges();
        }
    }
}
