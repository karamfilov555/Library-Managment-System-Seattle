using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
