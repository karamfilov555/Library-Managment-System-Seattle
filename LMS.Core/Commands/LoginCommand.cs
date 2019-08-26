using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IGlobalMessages _messages;
        public LoginCommand(IValidator validator,
                           ILoginAuthenticator login,
                           IGlobalMessages messages)
        {
            _validator = validator;
            _loginAuthenticator = login;
            _messages = messages;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAlreadyLoggedIn();
            _validator.LoginParametersCountValidation(parameteres);

            var username = parameteres[0];
            var password = parameteres[1];

            var user = _loginAuthenticator.CheckUserCredetials(username, password);
            _loginAuthenticator.SetCurrentUser(user, username, password);

            return _messages.SuccessfullyLoginMessage(username);
        }
    }
}
