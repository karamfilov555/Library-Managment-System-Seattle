using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Contracts
{
    public interface IReservationService
    {
        Task<ReserveBook> ReserveBookAsync(string bookId, string userId);
        Task<ReserveBook> CheckIfBookExistInReservations(string bookId);
        Task<IQueryable<ReserveBook>> GetReservationsOfUser(string userId);
        Task<ICollection<Book>> ExtractBooksFromReservation(IQueryable<ReserveBook> reserves);
    }
}
