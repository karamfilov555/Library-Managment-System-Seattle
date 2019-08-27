using Autofac;
using LMS.Core.Commands.Contracts;
using LMS.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core
{
    public class CommandFactory : ICommandFactory
    {
        private IComponentContext componentContext;

        public CommandFactory(IComponentContext context)
        {
            componentContext = context;
        }
        public ICommand FindCommand(string commandName)
        {
            ICommand command;
            //try
            //{
                command = componentContext
                    .ResolveNamed<ICommand>(commandName.ToLower());
            //}
            //catch (Exception)
            //{
            //    throw new ArgumentException("Invalid Command!");
            //}
            return command;
        }
    }
}
