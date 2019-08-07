using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface ICommandProcessor
    {
        string ProcessCommand(string consoleInput);
    }
}
