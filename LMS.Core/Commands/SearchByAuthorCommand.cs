using LMS.Core.Commands.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.Core.Contracts;
using LMS.Services.Contracts;

namespace LMS.Core.Commands
{
    public class SearchByAuthorCommand : ICommand
    {
        private readonly IInputReader _reader;
        private readonly IOutputWriter _writer;
        private readonly IBookServices _bookServices;

        public SearchByAuthorCommand(IInputReader reader, IOutputWriter writer, IBookServices bookServices)
        {
            _reader = reader;
            _writer = writer;
            _bookServices = bookServices;
        }
        public string Execute(IList<string> parameteres)
        {
            _writer.WriteLine("Enter author name:");
            var authorName = _reader.ReadLine();
            _bookServices.AllBooksToString(_bookServices.SearchByAuthor(authorName));
            return "searched books by author";
        }
    }
}
