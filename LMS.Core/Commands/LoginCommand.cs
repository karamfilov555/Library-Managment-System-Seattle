using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IGlobalMessages _messages;
        private readonly IRecordFinesServices _fineServices;
        private readonly IInputReader _reader;
        private readonly IInputKeyReader _keyReader;
        private readonly IOutputWriter _writer;
        public LoginCommand(ILoginAuthenticator login,
                           IGlobalMessages messages,
                           IRecordFinesServices fineServices,
                           IInputReader reader,
                           IOutputWriter writer,
                           IInputKeyReader keyReader)
        {
            _loginAuthenticator = login;
            _messages = messages;
            _fineServices = fineServices;
            _reader = reader;
            _writer = writer;
            _keyReader = keyReader;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAlreadyLoggedIn();

            _writer.Write("Username: ");
            var username = _reader.ReadLine();
            _writer.Write("Password/Input will be hidden/: ");
            var password = _keyReader.ReadKeys();
            _writer.WriteLine();
            var user = _loginAuthenticator.CheckUserCredetials(username, password);
            _loginAuthenticator.SetCurrentUser(user, username, password);

            return _messages.SuccessfullyLoginMessage(username) + Environment.NewLine + 
                _fineServices.GetUserTotalFineAmount(user);
        }
    }
}
