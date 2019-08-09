using LMS.Contracts;
using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    class MyBooksCommand : ICommand
    {
        private readonly IHistoryDataBase _history;
        public MyBooksCommand(IHistoryDataBase history)
        {
            _history = history;
        }
        public string Execute(IList<string> parameteres)
        {
            return _history.GetHistoryOfCurrentUser();
        }
    }
}
