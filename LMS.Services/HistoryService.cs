using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly LMSContext _context;

        public HistoryService(LMSContext context)
        {
            _context = context;
        }
        public async Task<HistoryRegistry> CheckoutBook(string bookId , string userId)
        {
           // TODO : null check here
            var historyRegistry = new HistoryRegistry()
            {
                BookId = bookId,
                UserId = userId,
                CheckOutDate = DateTime.Now.ToShortDateString(),
                ReturnDate = DateTime.Now.AddDays(10)
            };
            // TODO : null check here
            _context.HistoryRegistries.Add(historyRegistry);
            
            var book = await _context.Books.FirstAsync(x => x.Id == bookId);
            book.IsCheckedOut = true;
            var booksWithSameTitle = _context.Books.Where(b => b.Title == book.Title);
            foreach (var item in booksWithSameTitle)
            {
                item.Copies--;
            }
            await _context.SaveChangesAsync();
            return historyRegistry;
        }
    }
}
