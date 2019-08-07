using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.CommandContracts
{
    public interface ICommand
    {
        string Execute(IList<string> parameteres);
    }
}
