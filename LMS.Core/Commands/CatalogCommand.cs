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
        private readonly IBooksDataBase _booksDb;
        public CatalogCommand(IBooksDataBase booksDb, 
                              IValidator validator)
        {
            _booksDb = booksDb;
            _validator = validator;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.IsParametersCountIsValid(parameteres, 0);
            return _booksDb.AllExistingBooksToString();
        }
    }
}
