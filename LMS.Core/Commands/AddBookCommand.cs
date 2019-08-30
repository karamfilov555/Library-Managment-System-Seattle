using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IGlobalMessages _messages;
        private readonly IBookFactory _modelsFactory;
        private readonly IBookServices _bookServices;
        private readonly IInputReader _inputReader;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IOutputWriter _outputWriter;

        public AddBookCommand(IGlobalMessages globalMessages,
                              IBookFactory modelsFactory,
                              IInputReader inputReader,
                              ILoginAuthenticator loginAuthenticator,
                              IOutputWriter outputWriter,
                              IBookServices bookServices
                              )
        {
            _messages = globalMessages;
            _modelsFactory = modelsFactory;
            _inputReader = inputReader;
            _loginAuthenticator = loginAuthenticator;
            _outputWriter = outputWriter;
            _bookServices = bookServices;
        }
        public string Execute(IList<string> parameteres)
        {
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
                _outputWriter.WriteLine("Book's Title :");
                title = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book's Author :");
                author = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book's Pages :");
                pages = int.Parse(_inputReader.ReadLine());
                _outputWriter.WriteLine("Book's Year :");
                year = int.Parse(_inputReader.ReadLine());
                _outputWriter.WriteLine("Book's Country :");
                country = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book's Language :");
                language = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book's Subjects :");
                subject = _inputReader.ReadLine();
                _outputWriter.WriteLine("Book's Copies :");
                copies = int.Parse(_inputReader.ReadLine());
            }
            catch (Exception)
            {
                throw new ArgumentException(_messages.InvalidParametersMessage());
            }
            var subjects = subject.Split();
            var book = _modelsFactory.CreateBook(title,author,pages,year,country,language,subjects);

            _bookServices.AddBookToDb(book);

            return $"Book with title \"{title}\" was added!";
        }
    }
}
