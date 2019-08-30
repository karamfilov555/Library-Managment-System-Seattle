using LMS.Core.Commands.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class CatalogCommand : ICommand
    {
        private readonly IBookServices _bookServices;
        public CatalogCommand(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }
        public string Execute(IList<string> parameteres)
        {
           return _bookServices.AllBooksToString();
        }
    }
}
