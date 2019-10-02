using LMS.Data;
using LMS.Models;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
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
            var reserveBook = new ReserveBook
            {
                BookId = bookId,
                UserId = userId,
                BookTitle = book.Title,
                ReservationDate = DateTime.Now.ToShortDateString(),
            };

            await AddReservationToDb(reserveBook);

            return reserveBook;
        }
        private async Task AddReservationToDb(ReserveBook reservation)
        {
            _context.ReservedBooks.Add(reservation); //addAsync..nn
        }
        public async Task<ReserveBook> CheckIfBookExistInReservations(string bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            var bookInReservations = await _context.ReservedBooks.FirstOrDefaultAsync(b => b.BookTitle == book.Title);
            return bookInReservations;
        }
    }
}
