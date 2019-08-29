using System;
using System.Collections.Generic;
using System.Text;
using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Core.Commands
{
    public class ReserveBookCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IReserveBookFactory _reservationFactory;
        private readonly IReserveBookServices _reservationsServices;
        public ReserveBookCommand(IValidator validator, 
                                  IReserveBookFactory reservatioFactory, 
                                  IReserveBookServices reservationsServices)
        {
            _validator = validator;
            _reservationFactory = reservatioFactory;
            _reservationsServices = reservationsServices;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.CheckParametersCount(parameteres,2);
            var title = parameteres[0];
            var author = parameteres[1];
            var reservation = _reservationFactory.CreateReserveBook(title, author);
            _reservationsServices.AddReservationToDb(reservation);
            return $"You successfully reserved a book with title \"{title}\"";
        }
    }
}
