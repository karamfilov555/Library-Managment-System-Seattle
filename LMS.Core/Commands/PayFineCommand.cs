using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class PayFineCommand : ICommand
    {
        private readonly IInputReader _reader;
        private readonly IOutputWriter _writer;
        private readonly IRecordFinesServices _finesServices;
        private readonly ILoginAuthenticator _loginAuthenticator;
        public PayFineCommand(IInputReader reader,
                             IOutputWriter writer,
                             IRecordFinesServices finesServices,
                             ILoginAuthenticator loginAuthenticator)
        {
            _reader = reader;
            _writer = writer;
            _finesServices = finesServices;
            _loginAuthenticator = loginAuthenticator;
        }
        public string Execute(IList<string> parameteres)
        {
            _loginAuthenticator.IsAdmin();
            _writer.WriteLine("Username : ");
            var username = _reader.ReadLine();
            return _finesServices.PayFineToUser(username);
        }
    }
}
