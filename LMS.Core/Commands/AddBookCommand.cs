using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IValidator _validator;
        private readonly IGlobalMessages _messages;
        private readonly IModelsFactory _modelsFactory;
        private readonly IBooksDataBase _booksDataBase;
        private readonly IInputReader _inputReader;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IOutputWriter _outputWriter;

        public AddBookCommand(IValidator validator,
                              IGlobalMessages globalMessages,
                              IModelsFactory modelsFactory,
                              IInputReader inputReader,
                              ILoginAuthenticator loginAuthenticator,
                             IOutputWriter outputWriter,
                             IBooksDataBase booksDataBase)
        {
            _validator = validator;
            _messages = globalMessages;
            _modelsFactory = modelsFactory;
            _inputReader = inputReader;
            _loginAuthenticator = loginAuthenticator;
            _outputWriter = outputWriter;
            _booksDataBase = booksDataBase;
        }
        public string Execute(IList<string> parameteres)
        {
            _validator.IsParametersCountIsValid(parameteres, 0);
            _loginAuthenticator.IsAdmin();

            string title;
            string author;
            int pages;
            int year;
            string country;
            string language;
            string subject;
            int copies;

            try
            {
                _outputWriter.WriteLine("Book Title :");
                title = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book Author :");
                author = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book Pages :");
                pages = int.Parse(_inputReader.ReadLine());
                _outputWriter.WriteLine("Book Year :");
                year = int.Parse(_inputReader.ReadLine());
                _outputWriter.WriteLine("Book Country :");
                country = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book Language :");
                language = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book Subject :");
                subject = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book Copies :");
                copies = int.Parse(_inputReader.ReadLine());
            }
            catch (Exception)
            {
                throw new ArgumentException(_messages.InvalidParametersMessage());
            }
            var strBuilder = new StringBuilder();
            for (int i = 1; i <= copies; i++)
            {
                var book = _modelsFactory.CreateBook(title, author, pages, year, country, language, subject);
                _booksDataBase.AddBookToDb(book);
                _booksDataBase.AddBookToJsonDb(title, author, pages, year, country, language, subject);
                strBuilder.AppendLine(book.PrintBookInfo());
            }
                return _messages.PrintAddBookLabel() + strBuilder.ToString() ;
        }
    }
}
