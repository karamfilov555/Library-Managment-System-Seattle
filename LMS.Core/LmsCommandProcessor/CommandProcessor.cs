using LMS.Contracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Core.LmsCommandProcessor
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ITextManager _textManager;
        public CommandProcessor(ICommandFactory commandFactory, ITextManager textManager)
        {
            this._commandFactory = commandFactory;
            this._textManager = textManager;
        }
        public string ProcessCommand(string consoleInput)
        {
            var parameters = this._textManager.GetCommandParams(consoleInput);
            var commandName = this._textManager.ExtractCommandName(consoleInput);
            var command = this._commandFactory.FindCommand(commandName);
            var output = command.Execute(parameters.ToList());

            return output;
        }
    }
}
