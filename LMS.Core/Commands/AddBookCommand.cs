using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        public AddBookCommand()
        {

        }
        public string Execute(IList<string> parameteres)
        {
           return "AddBook !";
        }
    }
}
