using LMS.Core.Commands.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class MyBooksCommand : ICommand
    {
        private readonly IHistoryServices _historyServices;
        public MyBooksCommand(IHistoryServices historyServices)
        {
            _historyServices = historyServices;
        }
        public string Execute(IList<string> parameteres)
        {
            return _historyServices.AllUserBooksToString();
        }
    }
}
