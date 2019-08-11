using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private readonly IOutputWriter _outputWriter;
        private readonly IInputReader _reader;
        private readonly IHistoryServices _historyServices;
        private readonly IModelsFactory _factory;
        private readonly IGlobalMessages _message;
        private readonly IBookServices _bookServices;
        public ReturnBookCommand(IHistoryServices historyServices,
                                 IModelsFactory factory,
                                 IGlobalMessages message,
                                 IInputReader reader,
                                 IOutputWriter outputWriter,
                                 IBookServices bookServices)
        {
            _historyServices = historyServices;
            _factory = factory;
            _message = message;
            _outputWriter = outputWriter;
            _reader = reader;
            _bookServices = bookServices;
        }
        public string Execute(IList<string> parameteres)
        {
            _outputWriter.WriteLine(_historyServices.GetHistoryOfCurrentUser());
            _outputWriter.WriteLine(_message.WichBookYouWantToReturnMessage());
            var isbn = _reader.ReadLine();

            var bookToReturn = _historyServices.FindHistoryRegistry(isbn);
            _historyServices.RemoveFromHistory(bookToReturn);

            var book = _factory.CreateBook(bookToReturn.Title, bookToReturn.Author, bookToReturn.Pages, bookToReturn.Year, bookToReturn.Country, bookToReturn.Language,bookToReturn.Subject.ToString(),bookToReturn.ISBN);
            _bookServices.AddBookToDb(book);

            return _message.SuccessfullyReturnBookMessage(bookToReturn.Title);
        }
    }
}
