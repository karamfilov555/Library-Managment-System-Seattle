using LMS.Contracts;
using LMS.Core.CommandContracts;
using System.Collections.Generic;

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
            return _bookServices.AllExistingBooksToString();
        }
    }
}
