using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Contracts
{
    public interface ITextManager
    {
        string ExtractCommandName(string input);
        IEnumerable<string> GetCommandParams(string input);
        //bool LogInCommandExpected(string input);

    }
}
