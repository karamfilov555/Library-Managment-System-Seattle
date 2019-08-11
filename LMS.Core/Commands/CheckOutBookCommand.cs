using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    class CheckOutBookCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly ITextManager _textManager;
        private readonly IHistoryServices _historyServices;
        private readonly IModelsFactory _factory;
        private readonly IGlobalMessages _message;
        private readonly IBookServices _bookServices;
        private readonly IOutputWriter _writer;
        private readonly IInputReader _reader;
        public CheckOutBookCommand(IValidator validator, 
                                   ITextManager textManager,
                                   IHistoryServices historyServices,
                                   IModelsFactory factory,
                                   IGlobalMessages message,
                                   IBookServices bookServices,
                                   IOutputWriter writer,
                                   IInputReader reader)
        {
            _validator = validator;
            _textManager = textManager;
            _historyServices = historyServices;
            _factory = factory;
            _message = message;
            _bookServices = bookServices;
            _writer = writer;
            _reader = reader;
        }
        public string Execute(IList<string> parameteres)
        {
            var titleToCheckOut = _textManager.GetParams(parameteres);
            _historyServices.CheckBooksOfCurrentUser();
            _bookServices.FindBookInDb(titleToCheckOut);

            _writer.WriteLine(_bookServices.GiveAllBooksWithThisTitle(titleToCheckOut)+ $"{Environment.NewLine}" + "Type ISBN of book that you want to checkout: ");

            var isbn = _reader.ReadLine();
            var registry = _factory.CreateRegistry(titleToCheckOut , isbn);
            _historyServices.AddRegistryToHistoryDb(registry);

            return _message.BookCheckedOutMessage(registry.RegistryInfo());
        }
    }
}
