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
        private readonly IUsersDataBase _userDataBase;
        public RegisterCommand(IValidator validator,
                               IGlobalMessages messages,
                               ILoginAuthenticator loginAuthenticator,
                               IModelsFactory modelsFactory,
                               IUsersDataBase userDataBase)
        {
            _validator = validator;
            _messages = messages;
            _loginAuthenticator = loginAuthenticator;
            _modelsFactory = modelsFactory;
            _userDataBase = userDataBase;
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

            _userDataBase.AddUserToDb(newUser);

            return _messages.RegisterMessage(username);
        }
    }
}
