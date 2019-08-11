using LMS.Contracts;
using LMS.Core.CommandContracts;
using System.Collections.Generic;

namespace LMS.Core.Commands
{
    public class SearchByYearCommand : ICommand
    {
        private readonly IBookServices _bookServices;
        private readonly IValidator _validator;
        public SearchByYearCommand(IBookServices bookServices,
                                   IValidator validator)
        {
            _bookServices = bookServices;
            _validator = validator;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.SearchByYearParametersCountValidation(parameteres);
            _validator.TryParseToInt(parameteres[0]);
            var year = int.Parse(parameteres[0]);
            return _bookServices.ShowAllBooksWithThisYear(year);
        }
    }
}
