using LMS.Core.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Core.Commands
{
    public class RegisterCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IGlobalMessages _messages;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IUserFactory _usersFactory;
        private readonly IUserServices _usersServices;
        private const string defaultRoleName = "member";
        public RegisterCommand(IValidator validator,
            IGlobalMessages messages,
            ILoginAuthenticator loginAuthenticator,
            IUserFactory modelsFactory,
            IUserServices usersServices)
        {
            _validator = validator;
            _messages = messages;
            _loginAuthenticator = loginAuthenticator;
            _usersFactory = modelsFactory;
            _usersServices = usersServices;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAlreadyLoggedIn();
            _validator.RegisterParametersCountValidation(parameteres);
            var username = parameteres[0];
            var password = parameteres[1];

            var newUser = _usersFactory.CreateUser(username, password, defaultRoleName);
            _usersServices.AddUserToDb(newUser);
            return _messages.RegisterMessage(username);
        }
    }
}
