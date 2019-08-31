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
        private readonly IGlobalMessages _messages;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IUserFactory _usersFactory;
        private readonly IUserServices _usersServices;
        private readonly IInputReader _reader;
        private readonly IInputKeyReader _keyReader;
        private readonly IOutputWriter _writer;
        private const string defaultRoleName = "member";
        public RegisterCommand(
            IGlobalMessages messages,
            ILoginAuthenticator loginAuthenticator,
            IUserFactory modelsFactory,
            IUserServices usersServices,
            IInputReader reader,
            IInputKeyReader keyReader,
            IOutputWriter writer)
        {
            _messages = messages;
            _loginAuthenticator = loginAuthenticator;
            _usersFactory = modelsFactory;
            _usersServices = usersServices;
            _reader = reader;
            _keyReader = keyReader;
            _writer = writer;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAlreadyLoggedIn();
            _writer.Write("Username: ");
            var username = _reader.ReadLine();
            _writer.Write("Password/Input will be hidden/: ");
            var password = _keyReader.ReadKeys();
            _writer.WriteLine();

            var newUser = _usersFactory.CreateUser(username, password, defaultRoleName);
            _usersServices.AddUserToDb(newUser);
            return _messages.RegisterMessage(username);
        }
    }
}
