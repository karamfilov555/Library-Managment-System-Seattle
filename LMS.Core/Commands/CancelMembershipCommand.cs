using LMS.Contracts;
using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class CancelMembershipCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IGlobalMessages _message;
        private readonly ILoginAuthenticator _loginAuthenticator;
        public CancelMembershipCommand(IValidator validator, IGlobalMessages message, ILoginAuthenticator loginAuthenticator)
        {
            this._validator = validator;
            this._message = message;
            this._loginAuthenticator = loginAuthenticator;
        }
        public string Execute(IList<string> parameteres)
        {
            this._validator.IsParametersCountIsValid(parameteres , 1);
            this._message.CancelMemership_PasswordRequiredMessage();
            var password = parameteres[0];
            if (!this._loginAuthenticator.IsPasswordCorrect(password))
                return this._message.WrongPasswordMessage();

            this._loginAuthenticator.RemoveUserFromDb(this._loginAuthenticator.GetCurrentUserName());
            this._loginAuthenticator.LogoutCurrentUser();

            return this._message.CancelMemershipMessage();
        }
    }
}
