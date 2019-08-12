using LMS.Contracts;
using LMS.Core.CommandContracts;
using System.Collections.Generic;

namespace LMS.Core.Commands
{
    //Liskov(ICommand)
    public class CatalogCommand : ICommand
    {
        private readonly IBookServices _bookServices;
        public CatalogCommand(IBookServices bookServices) 
        {
            _bookServices = bookServices;
        }
        //Interface segregation (Execute from ICommand for 20 commands) 
        public string Execute(IList<string> parameteres)
        {
            return _bookServices.AllExistingBooksToString();
        }
        //Dependency Inversion with constr injection(bookServices) 
    }
}
