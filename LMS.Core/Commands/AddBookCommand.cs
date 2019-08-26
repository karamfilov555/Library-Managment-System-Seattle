using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using LMS.Data.Models.ModelsFactory;
using LMS.Generators.Contracts;
using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IGlobalMessages _messages;
        private readonly IModelsFactory _modelsFactory;
        private readonly IBookServices _bookServices;
        private readonly IInputReader _inputReader;
        //private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IOutputWriter _outputWriter;

        public AddBookCommand(IGlobalMessages globalMessages,
                              IModelsFactory modelsFactory,
                              IInputReader inputReader,
                              //ILoginAuthenticator loginAuthenticator,
                              IOutputWriter outputWriter,
                              IBookServices bookServices
                              )
        {
            _messages = globalMessages;
            _modelsFactory = modelsFactory;
            _inputReader = inputReader;
            //_loginAuthenticator = loginAuthenticator;
            _outputWriter = outputWriter;
            _bookServices = bookServices;
        }
        public string Execute(IList<string> parameteres)
        {
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

            var book = _modelsFactory.CreateBook(title,author,pages,year,country,language,subject);

            _bookServices.AddBookToDb(book);

            return "Book Added!";
        }
    }
}
