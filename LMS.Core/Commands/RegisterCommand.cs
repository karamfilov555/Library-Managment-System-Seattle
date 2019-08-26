using LMS.Core.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.Core.Contracts;
using LMS.Data.Models.ModelsFactory;
using LMS.Services.Contracts;

namespace LMS.Core.Commands
{
    public class RegisterCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IGlobalMessages _messages;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IModelsFactory _modelsFactory;
        private readonly IUserServices _usersServices;
        public RegisterCommand(IValidator validator,
            IGlobalMessages messages,
            ILoginAuthenticator loginAuthenticator,
            IModelsFactory modelsFactory,
            IUserServices usersServices)
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

            var newUser = _modelsFactory.CreateUser(username, password,"member");

            _usersServices.AddUserToDb(newUser);

            return _messages.RegisterMessage(username);
        }
    }
}
