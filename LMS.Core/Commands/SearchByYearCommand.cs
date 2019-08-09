using LMS.Contracts;
using LMS.Core.CommandContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    class SearchByYearCommand : ICommand
    {
        private readonly IBooksDataBase _booksDb;
        private readonly IValidator _validator;
        public SearchByYearCommand(IBooksDataBase booksDb, IValidator validator)
        {
            _booksDb = booksDb;
            _validator = validator;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.IsParametersCountIsValid(parameteres, 1);
            _validator.TryParseToInt(parameteres[0]);
            var year = int.Parse(parameteres[0]);
            return _booksDb.ShowAllBooksWithThisYear(year);
        }
    }
}
