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
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly ICommandProcessor _commandProcessor;
        public Engine(IOutputWriter outputWriter,
                      IInputReader inputReader,
                      IDataBaseLoader dataBaseLoader,
                      ILoginAuthenticator loginAuthenticator,
                      ICommandProcessor commandProcessor)
        {
            _outputWriter = outputWriter;
            _inputReader = inputReader;
            _loginAuthenticator = loginAuthenticator;
            _commandProcessor = commandProcessor;
        }
        public void Run()
        {
            string consoleInput = string.Empty;
            while ((consoleInput = _inputReader.ReadLine()) != "end")
            {
                try
                {
                    _loginAuthenticator.CheckAllowedCommands(consoleInput);
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
