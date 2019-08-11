using LMS.Contracts;
using LMS.Core.CommandContracts;
using System.Collections.Generic;

namespace LMS.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IGlobalMessages _messages;
        private readonly ILoginAuthenticator _loginAuthenticator;
        public LogoutCommand(IGlobalMessages messages, 
                             ILoginAuthenticator loginAuthenticator)
        {
            _messages = messages;
            _loginAuthenticator = loginAuthenticator;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.LogoutCurrentUser();
            return _messages.LogOutMessage();
        }
    }
}
