using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class CheckOutBookCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly ITextManager _textManager;
        private readonly IHistoryDataBase _history;
        private readonly IModelsFactory _factory;
        private readonly IGlobalMessages _message;
        public CheckOutBookCommand(IValidator validator, 
                                   ITextManager textManager, 
                                   IHistoryDataBase history,
                                   IModelsFactory factory,
                                   IGlobalMessages message)
        {
            _validator = validator;
            _textManager = textManager;
            _history = history;
            _factory = factory;
            _message = message;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.CheckOutBookParamsValidation(parameteres);
            var titleToCheckOut = _textManager.GetParams(parameteres);
            _history.CheckBooksOfCurrentUser();
            var registry = _factory.CreateRegistry(titleToCheckOut);
            _history.AddRegistryToHistoryDb(registry);

            return _message.BookCheckedOutMessage(registry.RegistryInfo());
        }
    }
}
