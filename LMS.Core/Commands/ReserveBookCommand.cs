using System.Collections.Generic;
using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Core.Commands
{
    public class ReserveBookCommand : ICommand
    {
        private readonly IReserveBookFactory _reservationFactory;
        private readonly IReserveBookServices _reservationsServices;
        private readonly IInputReader _reader;
        private readonly IOutputWriter _writer;
        public ReserveBookCommand(IInputReader reader,
                                  IOutputWriter writer,
                                  IReserveBookFactory reservationFactory, 
                                  IReserveBookServices reservationsServices)
        {
            _reservationFactory = reservationFactory;
            _reservationsServices = reservationsServices;
            _reader = reader;
            _writer = writer;
        }
        public string Execute(IList<string> parameteres)
        {
            _writer.WriteLine("Book's Title :");
            var title = _reader.ReadLine();
            _writer.WriteLine("Book's Author:");
            var author = _reader.ReadLine();
            var reservation = _reservationFactory.CreateReserveBook(title, author);
            _reservationsServices.AddReservationToDb(reservation);
            return $"You successfully reserved a book with title \"{title}\"";
        }
    }
}
