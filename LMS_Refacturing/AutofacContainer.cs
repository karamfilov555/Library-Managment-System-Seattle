using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LMS.Core.IO;
using System.Text;
using LMS.Core;
using LMS.Core.Factories;
using LMS.DataBase;
using LMS.Core.Commands;
using LMS.Core.CommandContracts;
using LMS.Contracts;
using LMS.Core.LmsEngine;
using LMS.Models;
using LMS.JasonDB;
using LMS.JasonDB.Contracts;

namespace LMS.Start
{
    public class AutofacContainer
    {
        public IContainer RegisterContainer()
        {
            var builder = new ContainerBuilder();

            Assembly commandsAssembly = Assembly.GetAssembly(typeof(LoginCommand));

            RegisterTypes(builder);

            RegisterCommandsWithStrings(builder, commandsAssembly);

            var container = builder.Build();

            return container;
        }
        private void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(InputReader)))
               .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Engine)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(CommandFactory)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BooksDataBase)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Book)))
              .AsImplementedInterfaces();


            builder.RegisterType<Json>()
             .As<IJson>().SingleInstance();

            builder.RegisterType<BooksDataBase>()
              .As<IBooksDataBase>().SingleInstance();

            builder.RegisterType<UsersDataBase>()
              .As<IUsersDataBase>().SingleInstance();

            builder.RegisterType<LoginAuthenticator>()
              .As<ILoginAuthenticator>().SingleInstance();

            builder.RegisterType<AdminsDataBase>()
                .As<IAdminsDataBase>().SingleInstance();
        }
        private void RegisterCommandsWithStrings(ContainerBuilder builder, Assembly commandsAssembly)
        {
            var commandTypes = commandsAssembly.DefinedTypes
               .Where(typeInfo => typeInfo.ImplementedInterfaces.Contains(typeof(ICommand)))
               .ToList();

            foreach (var commandType in commandTypes)
            {
                builder.RegisterType(commandType.AsType()).Named<ICommand>(commandType.Name.ToLower().Substring(0, commandType.Name.Length - 7));
            }
        }
    }
}
