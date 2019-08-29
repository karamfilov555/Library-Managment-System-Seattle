using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data;
using LMS.Models.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Services
{
    public class ReserveBookServices : IReserveBookServices
    {
        private readonly LMSContext _context;
        public ReserveBookServices(LMSContext context)
        {
            _context = context;
        }

        public void AddReservationToDb(ReserveBook reserveBook)
        {
            _context.Reservations.Add(reserveBook);
            _context.SaveChanges();
        }
    }
}
