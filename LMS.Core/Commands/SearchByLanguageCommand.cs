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
        private readonly IBooksDataBase _booksDb;
        private readonly ITextManager _textManager;
        public SearchByLanguageCommand(IBooksDataBase booksDb, ITextManager textManager)
        {
            _booksDb = booksDb;
            _textManager = textManager;
        }
        public string Execute(IList<string> parameteres)
        {
            var languageToSearchBy = _textManager.GetParams(parameteres);
            return _booksDb.ShowAllBooksWithThisLanguage(languageToSearchBy);
        }
    }
}
