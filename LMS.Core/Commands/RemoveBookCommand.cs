using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System.Collections.Generic;

namespace LMS.Core.Commands
{
    public class RemoveBookCommand : ICommand
    {
        private readonly IBookServices _bookServices;
        private readonly IGlobalMessages _message;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly ITextManager _textManager;

        public RemoveBookCommand(IBookServices bookServices, 
                                 IGlobalMessages message,
                                 ILoginAuthenticator loginAuthenticator,
                                 ITextManager textManager)
        {
            _bookServices = bookServices;
            _message = message;
            _loginAuthenticator = loginAuthenticator;
            _textManager = textManager;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAdmin();
            var title = _textManager.GetParams(parameteres);
            var bookToRemove = _bookServices.FindBookInDb(title);
            _bookServices.RemoveFromDb(bookToRemove);
            return _message.BookRemovedMessage(title);
        }
    }
}
