using LMS.Contracts;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
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
            this._validator = validator;
            this._messages = globalMessages;
            this._modelsFactory = modelsFactory;
            this._inputReader = inputReader;
            this._loginAuthenticator = loginAuthenticator;
            this._outputWriter = outputWriter;
            this._booksDataBase = booksDataBase;
        }
        public string Execute(IList<string> parameteres)
        {
            this._validator.IsParametersCountIsValid(parameteres, 0);
            this._loginAuthenticator.IsAdmin();

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
                this._outputWriter.WriteLine("Book Title :");
                title = this._inputReader.ReadLine();
                this._outputWriter.WriteLine("Book Author :");
                author = this._inputReader.ReadLine();
                this._outputWriter.WriteLine("Book Pages :");
                pages = int.Parse(this._inputReader.ReadLine());
                this._outputWriter.WriteLine("Book Year :");
                year = int.Parse(this._inputReader.ReadLine());
                this._outputWriter.WriteLine("Book Country :");
                country = this._inputReader.ReadLine();
                this._outputWriter.WriteLine("Book Language :");
                language = this._inputReader.ReadLine();
                this._outputWriter.WriteLine("Book Subject :");
                subject = this._inputReader.ReadLine();
                this._outputWriter.WriteLine("Book Copies :");
                copies = int.Parse(this._inputReader.ReadLine());
            }
            catch (Exception)
            {
                throw new ArgumentException(this._messages.InvalidParametersMessage());
            }

            var book = this._modelsFactory.CreateBook(title, author, pages, year, country, language, subject);

            this._booksDataBase.AddBookToDb(book);

            return this._messages.BookCreated();
        }
    }
}
