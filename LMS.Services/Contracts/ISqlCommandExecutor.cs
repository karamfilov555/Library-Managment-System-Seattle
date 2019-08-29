using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface ISqlCommandExecutor
    {
        void ExecuteCommand(StringBuilder sqlCommand);
    }
}
