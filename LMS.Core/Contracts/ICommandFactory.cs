using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand FindCommand(string commandName);
    }
}
