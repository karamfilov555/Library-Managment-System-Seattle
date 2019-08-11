using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System.Collections.Generic;

namespace LMS.Core.Commands
{
    public class CheckOutBookCommand : ICommand
    {
        private readonly ITextManager _textManager;
        private readonly IHistoryServices _historyServices;
        private readonly IModelsFactory _factory;
        private readonly IGlobalMessages _message;
        private readonly IBookServices _bookServices;
        public CheckOutBookCommand(ITextManager textManager,
                                   IHistoryServices historyServices,
                                   IModelsFactory factory,
                                   IGlobalMessages message,
                                   IBookServices bookServices)
        {
            _textManager = textManager;
            _historyServices = historyServices;
            _factory = factory;
            _message = message;
            _bookServices = bookServices;
        }
        public string Execute(IList<string> parameteres)
        {
            var titleToCheckOut = _textManager.GetParams(parameteres);
            _historyServices.CheckBooksOfCurrentUser();
            var book = _bookServices.FindBookInDb(titleToCheckOut);

            var registry = _factory.CreateRegistry(titleToCheckOut , book.ISBN);
            _historyServices.AddRegistryToHistoryDb(registry);
            _bookServices.RemoveFromDb(book);

            return _message.BookCheckedOutMessage(registry.RegistryInfo());
        }
    }
}
