using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class CheckOutBookCommand : ICommand
    {
        private readonly IHistoryRegistryFactory _historyFactory;
        private readonly IHistoryServices _historyServices;
        private readonly IInputReader _reader;
        private readonly IOutputWriter _writer;
        public CheckOutBookCommand(IInputReader reader,
                                  IOutputWriter writer,
                                  IHistoryRegistryFactory historyFactory,
                                  IHistoryServices historyServices)
        {
            _historyFactory = historyFactory;
            _historyServices = historyServices;
            _reader = reader;
            _writer = writer;
        }
        public string Execute(IList<string> parameteres)
        {
            _writer.WriteLine("Book's Title :");
            var title = _reader.ReadLine();
            _writer.WriteLine("Book's Author:");
            var author = _reader.ReadLine();

            var checkOut = _historyFactory.CreateHistoryRegistry(title, author);
            _historyServices.AddHistoryToDb(checkOut);
            return $"You successfully check-out a book with title \"{title}\"";
        }
    }
}
