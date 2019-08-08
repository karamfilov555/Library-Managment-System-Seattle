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
            this._bookDb = bookDb;
            this._message = message;
            this._loginAuthenticator = loginAuthenticator;
            this._textManager = textManager;
            this._json = json;
        }
        public string Execute(IList<string> parameteres)
        {
            this._loginAuthenticator.IsAdmin();
            var title = this._textManager.GetParams(parameteres);
            var bookToRemove = this._bookDb.FindBookInDb(title);
            this._bookDb.RemoveFromDb(bookToRemove);
            this._json.RemoveBookFromJsonDb(title);

            return this._message.BookRemovedMessage(title);
        }
    }
}
