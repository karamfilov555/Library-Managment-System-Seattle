using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System.Collections.Generic;

namespace LMS.Core.Commands
{
    public class SearchByTitleCommand : ICommand
    {
        private readonly IBookServices _bookServices;
        private readonly ITextManager _textManager;
        public SearchByTitleCommand(IBookServices bookServices, 
                                    ITextManager textManager)
        {
            _bookServices = bookServices;
            _textManager = textManager;
        }
        public string Execute(IList<string> parameteres)
        {
            var titleToSearchBy = _textManager.GetParams(parameteres);
            return _bookServices.ShowAllBooksWithThisTitle(titleToSearchBy);
        }
    }
}
