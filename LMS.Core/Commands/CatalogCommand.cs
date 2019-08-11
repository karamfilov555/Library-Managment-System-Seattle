using LMS.Contracts;
using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class CatalogCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IBookServices _bookServices;
        public CatalogCommand(IBookServices bookServices, 
                              IValidator validator)
        {
            _bookServices = bookServices;
            _validator = validator;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.IsParametersCountIsValid(parameteres, 0);
            return _bookServices.AllExistingBooksToString();
        }
    }
}
