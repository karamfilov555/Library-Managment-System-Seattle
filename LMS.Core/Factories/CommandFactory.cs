using Autofac;
using LMS.Core.CommandContracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private IComponentContext componentContext;

        public CommandFactory(IComponentContext context)
        {
            this.componentContext = context;
        }
        public ICommand FindCommand(string commandName)
        {
            ICommand command;
            try
            {
                 command = this.componentContext
                    .ResolveNamed<ICommand>(commandName.ToLower());
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException();
            }
            return command;

        }
    }
}
