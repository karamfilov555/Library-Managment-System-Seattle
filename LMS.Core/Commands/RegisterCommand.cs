using LMS.Contracts;
using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    class RegisterCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IGlobalMessages _messages;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IModelsFactory _modelsFactory;
        private readonly IUsersServices _usersServices;
        public RegisterCommand(IValidator validator,
                               IGlobalMessages messages,
                               ILoginAuthenticator loginAuthenticator,
                               IModelsFactory modelsFactory,
                               IUsersServices usersServices)
        {
            _validator = validator;
            _messages = messages;
            _loginAuthenticator = loginAuthenticator;
            _modelsFactory = modelsFactory;
            _usersServices = usersServices;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAlreadyLoggedIn();
            _validator.RegisterParametersCountValidation(parameteres);

            var username = parameteres[0];
            var password = parameteres[1];

            if (_loginAuthenticator.CheckUsernameInAdminDb(username))
                return _messages.ThisUserAlreadyExistMessage();

            if (_loginAuthenticator.CheckUsernameInUserDb(username))
                return _messages.ThisUserAlreadyExistMessage();

            var newUser  = _modelsFactory.CreateUser(username, password);

            _usersServices.AddUserToDb(newUser);

            return _messages.RegisterMessage(username);
        }
    }
}
