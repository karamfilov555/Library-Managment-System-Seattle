using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using LMS.JasonDB.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Core.Commands
{
    public class RemoveBookCommand : ICommand
    {
        private readonly IBooksDataBase _bookDb;
        private readonly IGlobalMessages _message;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly ITextManager _textManager;
        private readonly IJson _json;

        public RemoveBookCommand(IBooksDataBase bookDb, 
                                 IGlobalMessages message,
                                 ILoginAuthenticator loginAuthenticator,
                                 ITextManager textManager,
                                 IJson json)
        {
            _bookDb = bookDb;
            _message = message;
            _loginAuthenticator = loginAuthenticator;
            _textManager = textManager;
            _json = json;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAdmin();
            var title = _textManager.GetParams(parameteres);
            var bookToRemove = _bookDb.FindBookInDb(title);
            _bookDb.RemoveFromDb(bookToRemove);
            _json.RemoveBookFromJsonDb(title);

            return _message.BookRemovedMessage(title);
        }
    }
}
