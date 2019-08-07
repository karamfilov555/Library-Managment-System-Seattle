using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class CheckOutBookCommand : ICommand
    {
        public CheckOutBookCommand()
        {

        }
        public string Execute(IList<string> parameteres)
        {
            return "checkOutBook!";
        }
    }
}
