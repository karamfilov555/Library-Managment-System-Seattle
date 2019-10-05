using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly LMSContext _context;
        private readonly IBookService _bookService;

        public HistoryService(LMSContext context,
                              IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }
        public async Task<HistoryRegistry> CheckoutBookAsync(string bookId, string userId)
        {
            if (bookId == null)
                throw new ArgumentException("Invalid checkout parameters: book id cannot be null.");
            if (userId == null)
                throw new ArgumentException("Invalid checkout parameters: user id cannot be null.");

            var historyRegistry = new HistoryRegistry()
            {
                BookId = bookId,
                UserId = userId,
                CheckOutDate = DateTime.Now.ToShortDateString(),
                ReturnDate = DateTime.Now.AddDays(10)
            };

            if (historyRegistry == null)
                throw new ArgumentException("Invalid operation: history registry cannot be null.");
            
            _context.HistoryRegistries.Add(historyRegistry);

            var book = await _context.Books.FirstAsync(x => x.Id == bookId);
            book.IsCheckedOut = true;
            //avlb. copies prop. of this book decr. with 1 in each book 
            await _context.Books.Where(b => b.Title == book.Title && b.Author.Name == book.Author.Name && b.Language == book.Language).ForEachAsync(bc => bc.Copies--);

            await _context.SaveChangesAsync();
            return historyRegistry;
        }
        public async Task<IDictionary<Book, DateTime>> GetCheckOutsOfUserAsync(string userId)
        {
            var hrOfNotReturnedBooks = await GetUserHistoryOfNotReturnedBooksAsync(userId);

            var currentUserBooksAndReturnDates = new Dictionary<Book, DateTime>();

            foreach (var item in hrOfNotReturnedBooks)
            {
                var book = await _bookService.FindByIdAsync(item.BookId);
                var returnDate = item.ReturnDate;
                currentUserBooksAndReturnDates.Add(book, returnDate);
            }
            return currentUserBooksAndReturnDates;
        }

        private async Task<IList<HistoryRegistry>> GetUserHistoryOfNotReturnedBooksAsync(string userId)
        => await _context.HistoryRegistries
                            .Where(hr => hr.UserId == userId && hr.IsReturned == false)
                            .ToListAsync();
        private async Task<HistoryRegistry> GetHistoryRegistryAsync(string bookId, string userId)
        => await _context.HistoryRegistries
                         .FirstAsync(hr => hr.BookId == bookId && hr.UserId == userId && hr.IsReturned == false);
        public async Task AutoReturnAllBooksOfUser(string userId)
        {
            var hrOfUser = await GetUserHistoryOfNotReturnedBooksAsync(userId);
            foreach (var history in hrOfUser)
            {
                await ReturnBookAsync(history.BookId, history.UserId);
            }
        }
        public async Task ReturnBookAsync(string bookId, string userId)
        {
            if (bookId == null)
                throw new ArgumentException();

            var hr = await GetHistoryRegistryAsync(bookId, userId);
            _context.HistoryRegistries.Remove(hr);

            var book = await _bookService.FindByIdAsync(bookId);
            book.IsCheckedOut = false;

            await _context.Books.Where(b => b.Title == book.Title && b.Author.Name == book.Author.Name && b.Language == book.Language).ForEachAsync(bc => bc.Copies++);

            await _context.SaveChangesAsync();
        }
        public async Task<HistoryRegistry> RenewBookAsync(string bookId, string userId)
        {
            if (bookId == null)
                throw new ArgumentException();

            var hr = await GetHistoryRegistryAsync(bookId, userId);
            hr.ReturnDate = DateTime.Now.AddDays(10);

            await _context.SaveChangesAsync();
            return hr;
        }
    }
}
