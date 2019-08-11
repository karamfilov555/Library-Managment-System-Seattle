using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    class SearchByLanguageCommand : ICommand
    {
        private readonly IBookServices _bookServices;
        private readonly ITextManager _textManager;
        public SearchByLanguageCommand(IBookServices bookServices, 
                                       ITextManager textManager)
        {
            _bookServices = bookServices;
            _textManager = textManager;
        }
        public string Execute(IList<string> parameteres)
        {
            var languageToSearchBy = _textManager.GetParams(parameteres);
            return _bookServices.ShowAllBooksWithThisLanguage(languageToSearchBy);
        }
    }
}
