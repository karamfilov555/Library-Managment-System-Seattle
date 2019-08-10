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
            _validator = validator;
            _message = message;
            _loginAuthenticator = loginAuthenticator;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.IsParametersCountIsValid(parameteres , 1);
            _message.CancelMemership_PasswordRequiredMessage();
            var password = parameteres[0];
            if (!_loginAuthenticator.IsPasswordCorrect(password))
                return _message.WrongPasswordMessage();

            _loginAuthenticator.RemoveUserFromDb(_loginAuthenticator.GetCurrentUserName());
            _loginAuthenticator.LogoutCurrentUser();

            return _message.CancelMemershipMessage();
        }
    }
}
