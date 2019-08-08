using LMS.Contracts;
using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class RegisterCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IGlobalMessages _messages;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IModelsFactory _modelsFactory;
        public RegisterCommand(IValidator validator,
                               IGlobalMessages messages,
                               ILoginAuthenticator loginAuthenticator,
                               IModelsFactory modelsFactory)
        {
            this._validator = validator;
            this._messages = messages;
            this._loginAuthenticator = loginAuthenticator;
            this._modelsFactory = modelsFactory;
        }
        public string Execute(IList<string> parameteres)
        {
            this._validator.IsAlreadyLoggedIn();
            this._validator.IsParametersCountIsValid(parameteres, 2);

            var username = parameteres[0];
            var password = parameteres[1];

            var admin = this._loginAuthenticator.CheckUsernameInAdminDb(username);
            if (!this._validator.IsNull(admin))
                return this._messages.ThisUserAlreadyExistMessage();

            var user = this._loginAuthenticator.CheckUsernameInUserDb(username);
            if (!this._validator.IsNull(user))
                return this._messages.ThisUserAlreadyExistMessage();

            var newUser  = this._modelsFactory.CreateUser(username, password);

            return this._messages.RegisterMessage(username);
        }
    }
}
