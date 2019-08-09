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
        private readonly IDataBaseLoader _dataBaseLoader;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IGlobalMessages _globalMessages;
        private readonly ICommandProcessor _commandProcessor;
        public Engine(IOutputWriter outputWriter,
                      IInputReader inputReader,
                      IDataBaseLoader dataBaseLoader,
                      ILoginAuthenticator loginAuthenticator,
                      IGlobalMessages globalMessages,
                      ICommandProcessor commandProcessor)
        {
            _outputWriter = outputWriter;
            _inputReader = inputReader;
            _dataBaseLoader = dataBaseLoader;
            _loginAuthenticator = loginAuthenticator;
            _globalMessages = globalMessages;
            _commandProcessor = commandProcessor;
        }
        public void Run()
        {
            _dataBaseLoader.FillDataBase();

            string consoleInput = string.Empty;
            while ((consoleInput = _inputReader.ReadLine()) != "end")
            {
                try
                {
                    if (!_loginAuthenticator.CheckAllowedCommands(consoleInput))
                    {
                        _outputWriter.WriteLine
                            (_globalMessages.PleaseLoginOrRegisterMessage());
                        continue;
                    }
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
