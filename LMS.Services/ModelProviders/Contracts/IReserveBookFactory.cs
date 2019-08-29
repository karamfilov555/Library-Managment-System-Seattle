using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models.Models;

namespace LMS.Services.ModelProviders.Contracts
{
    public interface IReserveBookFactory
    {
        ReserveBook CreateReserveBook(string title, string author);
    }
}
