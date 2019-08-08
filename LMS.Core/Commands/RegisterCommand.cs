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
        private readonly IUsersDataBase _userDataBase;
        public RegisterCommand(IValidator validator,
                               IGlobalMessages messages,
                               ILoginAuthenticator loginAuthenticator,
                               IModelsFactory modelsFactory,
                               IUsersDataBase userDataBase)
        {
            this._validator = validator;
            this._messages = messages;
            this._loginAuthenticator = loginAuthenticator;
            this._modelsFactory = modelsFactory;
            this._userDataBase = userDataBase;
        }
        public string Execute(IList<string> parameteres)
        {
            this._validator.IsAlreadyLoggedIn();
            this._validator.IsParametersCountIsValid(parameteres, 2);

            var username = parameteres[0];
            var password = parameteres[1];

            if (this._loginAuthenticator.CheckUsernameInAdminDb(username))
                return this._messages.ThisUserAlreadyExistMessage();

            if (this._loginAuthenticator.CheckUsernameInUserDb(username))
                return this._messages.ThisUserAlreadyExistMessage();

            var newUser  = this._modelsFactory.CreateUser(username, password);

            this._userDataBase.AddUserToDb(newUser);

            return this._messages.RegisterMessage(username);
        }
    }
}
