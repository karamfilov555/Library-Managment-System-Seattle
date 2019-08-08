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
            this._validator = validator;
            this._loginAuthenticator = login;
            this._messages = messages;
        }
        public string Execute(IList<string> parameteres)
        {
            this._loginAuthenticator.IsAlreadyLoggedIn();
            this._validator.LoginParametersCountValidation(parameteres);
            var username = parameteres[0];
            var password = parameteres[1];

            var user = this._loginAuthenticator.CheckUserCredetials(username, password);
            if (!this._validator.IsNull(user))
                this._loginAuthenticator.SetCurrentUser(user, username, password);

            var admin = this._loginAuthenticator.CheckAdminCredetials(username, password);
            if (!this._validator.IsNull(admin))
                this._loginAuthenticator.SetCurrentUser(admin, username, password);

            if (this._validator.IsNull(this._loginAuthenticator.GetCurrentUser()))
                return this._messages.WrongCredentialsMessage();

            return this._messages.SuccessfullyLoginMessage(username);
        }
    }
}
