using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models.Models;

namespace LMS.Services.Contracts
{
    public interface IReserveBookServices
    {
        void AddReservationToDb(ReserveBook reserveBook);
    }
}
