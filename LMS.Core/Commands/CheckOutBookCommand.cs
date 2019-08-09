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
        private readonly IHistoryDataBase _history;
        private readonly IModelsFactory _factory;
        private readonly IGlobalMessages _message;
        private readonly IBooksDataBase _bookDb;
        private readonly IOutputWriter _writer;
        private readonly IInputReader _reader;
        public CheckOutBookCommand(IValidator validator, 
                                   ITextManager textManager, 
                                   IHistoryDataBase history,
                                   IModelsFactory factory,
                                   IGlobalMessages message,
                                   IBooksDataBase bookDb,
                                   IOutputWriter writer,
                                   IInputReader reader)
        {
            _validator = validator;
            _textManager = textManager;
            _history = history;
            _factory = factory;
            _message = message;
            _bookDb = bookDb;
            _writer = writer;
            _reader = reader;
        }
        public string Execute(IList<string> parameteres)
        {
            var titleToCheckOut = _textManager.GetParams(parameteres);
            _history.CheckBooksOfCurrentUser();
            _bookDb.FindBookInDb(titleToCheckOut);

            _writer.WriteLine(_bookDb.GiveAllBooksWithThisTitle(titleToCheckOut)+ $"{Environment.NewLine}" + "Type ISBN of book that you want to checkout: ");

            var isbn = _reader.ReadLine();
            var registry = _factory.CreateRegistry(titleToCheckOut , isbn);
            _history.AddRegistryToHistoryDb(registry);

            return _message.BookCheckedOutMessage(registry.RegistryInfo());
        }
    }
}
