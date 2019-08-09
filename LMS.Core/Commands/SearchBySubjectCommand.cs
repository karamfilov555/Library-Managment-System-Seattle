using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    class SearchBySubjectCommand : ICommand
    {
        private readonly IBooksDataBase _booksDb;
        private readonly ITextManager _textManager;
        public SearchBySubjectCommand(IBooksDataBase booksDb, ITextManager textManager)
        {
            _booksDb = booksDb;
            _textManager = textManager;
        }
        public string Execute(IList<string> parameteres)
        {
            var subjectToSearchBy = _textManager.GetParams(parameteres);
            return _booksDb.ShowAllBooksWithThisSubject(subjectToSearchBy);
        }
    }
}
