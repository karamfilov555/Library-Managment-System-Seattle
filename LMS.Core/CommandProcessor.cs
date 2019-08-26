using LMS.Core.Contracts;
using System;
using System.Linq;

namespace LMS.Core
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ITextManager _textManager;
        public CommandProcessor(ICommandFactory commandFactory, ITextManager textManager)
        {
            _commandFactory = commandFactory;
            _textManager = textManager;
        }
        public string ProcessCommand(string consoleInput)
        {
            var parameters = _textManager.GetCommandParams(consoleInput);
            var commandName = _textManager.ExtractCommandName(consoleInput);
            var command = _commandFactory.FindCommand(commandName);
            var output = command.Execute(parameters.ToList());

            return output;
        }
    }
}
