using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private readonly IHistoryServices _historyServices;
        private readonly IInputReader _reader;
        private readonly IOutputWriter _writer;
        public ReturnBookCommand(IInputReader reader,
                                  IOutputWriter writer,
                                  IHistoryServices historyServices)
        {
            _historyServices = historyServices;
            _reader = reader;
            _writer = writer;
        }
        public string Execute(IList<string> parameteres)
        {
            _writer.WriteLine("Book's Title :");
            var title = _reader.ReadLine();

            _historyServices.ReturnBook(title);
            return $"You successfully return a book with title \"{title}\"";
        }
    }
}
