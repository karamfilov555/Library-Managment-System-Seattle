using LMS.Contracts;
using LMS.Core.Contracts;
using System;
using System.Linq;

namespace LMS.Core.LmsEngine
{
    public class Engine : IEngine
    {
        private readonly IInputReader _inputReader;
        private readonly IOutputWriter _outputWriter;
        private readonly IBooksDataBase _booksDataBase;
        private readonly IAdminsDataBase _adminsDataBase;
        private readonly IUsersDataBase _usersDataBase;
        private readonly ILoginAuthenticator _currentUser;
        private readonly IValidator _validator;
        private readonly IGlobalMessages _globalMessages;
        private readonly ICommandProcessor _commandProcessor;
        public Engine(IOutputWriter outputWriter,
                      IInputReader inputReader,
                      IBooksDataBase booksDB,
                      IAdminsDataBase adminsDataBase,
                      IUsersDataBase usersDataBase,
                      ILoginAuthenticator currentUser,
                      IValidator validator,
                      IGlobalMessages globalMessages,
                      ICommandProcessor commandProcessor)
        {
            _outputWriter = outputWriter;
            _inputReader = inputReader;
            _booksDataBase = booksDB;
            _adminsDataBase = adminsDataBase;
            _usersDataBase = usersDataBase;
            _currentUser = currentUser;
            _validator = validator;
            _globalMessages = globalMessages;
            _commandProcessor = commandProcessor;
        }
        public void Run()
        {
            _booksDataBase.LoadBooksFromJson();
            _adminsDataBase.LoadAdminsFromJson();
            _usersDataBase.LoadUsersFromJson();

            string consoleInput = string.Empty;
            while ((consoleInput = _inputReader.ReadLine()) != "end")
            {
                try
                {
                    //How to extract this...
                    var currentUser = _currentUser.GetCurrentUser();
                    if (_validator.IsNull(currentUser) 
                        && !_validator.CommandNameIsLogin(consoleInput)
                        && !_validator.CommandNameIsRegister(consoleInput))
                    {
                        _outputWriter.WriteLine
                            (_globalMessages.PleaseLoginOrRegisterMessage());
                        continue;
                    }
                    //...in Method ?
                    var output = _commandProcessor.ProcessCommand(consoleInput);
                    _outputWriter.WriteLine(output);
                }
                catch (ArgumentException ex)
                {
                    _outputWriter.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }
    }
}
