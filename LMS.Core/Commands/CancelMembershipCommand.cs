using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
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
        private readonly IUserServices _userServices;
        public CancelMembershipCommand(IValidator validator,
                                       IGlobalMessages message,
                                       ILoginAuthenticator loginAuthenticator,
                                       IUserServices userServices)
        {
            _validator = validator;
            _message = message;
            _loginAuthenticator = loginAuthenticator;
            _userServices = userServices;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.CancelMembershipCountValidation(parameteres);
            _message.CancelMemership_PasswordRequiredMessage();
            var password = parameteres[0];
            _loginAuthenticator.IsPasswordCorrect(password);

            var currentUser = _loginAuthenticator.LoggedUser();
            _userServices.RemoveUserFromDb(currentUser);
            _loginAuthenticator.LogoutCurrentUser();
            return _message.CancelMemershipMessage();
        }
    }
}
