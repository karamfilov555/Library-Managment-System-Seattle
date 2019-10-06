using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LMS.Services
{
    public class ReservationService : IReservationService
    {
        private readonly LMSContext _context;
        private readonly IBookService _bookService;

        public ReservationService(LMSContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }
        public async Task<ReserveBook> ReserveBookAsync(string bookId, string userId)
        {
            var book = await _context.Books.FindAsync(bookId).ConfigureAwait(false);
            var reservation = await _context.ReservedBooks.FirstOrDefaultAsync(r => r.BookId == bookId && r.UserId == userId).ConfigureAwait(false);
            if (reservation != null)
            {
                return null;
            }
            else
            {
                reservation = new ReserveBook
                {
                    BookId = bookId,
                    UserId = userId,
                    BookTitle = book.Title,
                    ReservationDate = DateTime.Now,
                };
                await AddReservationToDb(reservation).ConfigureAwait(false);
            }

            return reservation;
        }
        private async Task AddReservationToDb(ReserveBook reservation)
        {
            _context.ReservedBooks.Add(reservation); //addAsync..nn
        }

        private async Task<bool> CheckIfBookWithThisTitleExist(string title)
        => await _context.ReservedBooks.AnyAsync(b => b.BookTitle == title).ConfigureAwait(false);

        public async Task<ReserveBook> CheckIfBookExistInReservations(string bookId)
        {
            var book = await _context.Books.FindAsync(bookId).ConfigureAwait(false);

            ReserveBook reservation = null;

            if (await CheckIfBookWithThisTitleExist(book.Title).ConfigureAwait(false))
            {
                var bookInReservations = _context.ReservedBooks.Where(b => b.BookTitle == book.Title);
                var dateOfFirstReservation = await bookInReservations.MinAsync(d => d.ReservationDate).ConfigureAwait(false);
                reservation = await _context.ReservedBooks.FirstAsync(d => d.ReservationDate == dateOfFirstReservation).ConfigureAwait(false);
                await GiveBookToFirstReservation(reservation.BookId, reservation.UserId).ConfigureAwait(false);
                _context.ReservedBooks.Remove(reservation);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            return reservation;
        }
        private async Task GiveBookToFirstReservation(string bookId, string userId)
        {
            if (bookId == null)
                throw new ArgumentException("Invalid parameters: book id cannot be null.");
            if (userId == null)
                throw new ArgumentException("Invalid parameters: user id cannot be null.");
            //remove old history registry
            var oldHistoryRegistry = await _context.HistoryRegistries.FirstAsync(hr => hr.BookId == bookId).ConfigureAwait(false);
            _context.HistoryRegistries.Remove(oldHistoryRegistry);
            await _context.SaveChangesAsync().ConfigureAwait(false);

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
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }


        public async Task<IQueryable<ReserveBook>> GetReservationsOfUser(string userId)
        {
            if (userId == null)
                throw new ArgumentException("User id cannot be null");

            var reservations = _context.ReservedBooks.Where(r => r.UserId == userId);
            return reservations;
        }
        public async Task<ICollection<Book>> ExtractBooksFromReservation(IQueryable<ReserveBook> reserves)
        {
            var booksFromReservations = new List<Book>();
            foreach (var item in reserves)
            {
                var book = await _bookService.FindByIdAsync(item.BookId).ConfigureAwait(false);
                booksFromReservations.Add(book);
            }

            return booksFromReservations;
        }
        
    }
}
