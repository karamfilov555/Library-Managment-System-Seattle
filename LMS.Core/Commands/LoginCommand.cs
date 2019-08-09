using LMS.Contracts;
using LMS.Core.CommandContracts;
using System.Collections.Generic;

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
            if (!_validator.IsNull(user))
                _loginAuthenticator.SetCurrentUser(user, username, password);

            var admin = _loginAuthenticator.CheckAdminCredetials(username, password);
            if (!_validator.IsNull(admin))
                _loginAuthenticator.SetCurrentUser(admin, username, password);

            if (_validator.IsNull(_loginAuthenticator.GetCurrentUser()))
                return _messages.WrongCredentialsMessage();

            return _messages.SuccessfullyLoginMessage(username);
        }
    }
}
