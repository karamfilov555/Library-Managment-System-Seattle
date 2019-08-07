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
            this._outputWriter = outputWriter;
            this._inputReader = inputReader;
            this._booksDataBase = booksDB;
            this._adminsDataBase = adminsDataBase;
            this._usersDataBase = usersDataBase;
            this._currentUser = currentUser;
            this._validator = validator;
            this._globalMessages = globalMessages;
            this._commandProcessor = commandProcessor;
        }
        public void Run()
        {
            this._booksDataBase.LoadBooksFromJson();
            this._adminsDataBase.LoadAdminsFromJson();
            this._usersDataBase.LoadUsersFromJson();

            string consoleInput = string.Empty;
            while ((consoleInput = this._inputReader.ReadLine()) != "end")
            {
                try
                {
                    //How to extract this...
                    var currentUser = this._currentUser.GetCurrentUser();
                    if (this._validator.IsNull(currentUser) 
                        && !this._validator.CommandNameIsLogin(consoleInput)
                        && !this._validator.CommandNameIsRegister(consoleInput))
                    {
                        this._outputWriter.WriteLine
                            (this._globalMessages.PleaseLoginOrRegisterMessage());
                        continue;
                    }
                    //...in Method ?
                    var output = this._commandProcessor.ProcessCommand(consoleInput);
                    this._outputWriter.WriteLine(output);
                }
                catch (ArgumentException ex)
                {
                    this._outputWriter.WriteLine($"ERROR: {ex.Message}");
                }
            }
        }
    }
}
