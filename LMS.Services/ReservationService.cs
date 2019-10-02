using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services
{
    public class ReservationService : IReservationService
    {
        private readonly LMSContext _context;

        public ReservationService(LMSContext context)
        {
            _context = context;
        }
        public async Task<ReserveBook> ReserveBookAsync(string bookId, string userId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var reservation = await _context.ReservedBooks.FirstOrDefaultAsync(r => r.BookId == bookId && r.UserId == userId);
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
                await AddReservationToDb(reservation);
            }

            return reservation;
        }
        private async Task AddReservationToDb(ReserveBook reservation)
        {
            _context.ReservedBooks.Add(reservation); //addAsync..nn
        }

        private async Task<bool> CheckIfBookWithThisTitleExist(string title)
        => await _context.ReservedBooks.AnyAsync(b => b.BookTitle == title);


        public async Task<ReserveBook> CheckIfBookExistInReservations(string bookId)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (await CheckIfBookWithThisTitleExist(book.Title))
            {
                var bookInReservations = _context.ReservedBooks.Where(b => b.BookTitle == book.Title);
                var dateOfFirstReservation = await bookInReservations.MinAsync(d => d.ReservationDate);
                var reservation = await _context.ReservedBooks.FirstAsync(d => d.ReservationDate == dateOfFirstReservation);
                await GiveBookToFirstReservation(reservation.BookId, reservation.UserId);
                _context.ReservedBooks.Remove(reservation);
                await _context.SaveChangesAsync();
                return reservation;
            }
            return null;
        }
        private async Task GiveBookToFirstReservation(string bookId, string userId)
        {
            if (bookId == null)
                throw new ArgumentException("Invalid parameters: book id cannot be null.");
            if (userId == null)
                throw new ArgumentException("Invalid parameters: user id cannot be null.");

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
            await _context.SaveChangesAsync();
        }

    }
}
