using Autofac;
using System;
using System.Linq;
using System.Reflection;
using LMS.Core.IO;
using LMS.Core.Factories;
using LMS.Services;
using LMS.Core.Commands;
using LMS.Core.CommandContracts;
using LMS.Contracts;
using LMS.Core.LmsEngine;
using LMS.Models;
using LMS.Generators;
using LMS.Generators.Contracts;
using LMS.JsonDB;
using LMS.Contracts.DataBaseContracts;

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

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(BookServices)))
              .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Book)))
              .AsImplementedInterfaces();

            builder.RegisterType<BookDataBase>()
             .As<IBookDataBase>().SingleInstance();

            builder.RegisterType<AdminDataBase>()
            .As<IAdminDataBase>().SingleInstance();

            builder.RegisterType<UserDataBase>()
           .As<IUserDataBase>().SingleInstance();

            builder.RegisterType<HistoryDataBase>()
           .As<IHistoryDataBase>().SingleInstance();

            builder.RegisterType<BookServices>()
              .As<IBookServices>().SingleInstance();

            builder.RegisterType<UsersServices>()
              .As<IUsersServices>().SingleInstance();

            builder.RegisterType<LoginAuthenticator>()
              .As<ILoginAuthenticator>().SingleInstance();

            builder.RegisterType<AdminServices>()
                .As<IAdminServices>().SingleInstance();

            builder.RegisterType<DataBaseLoader>()
                .As<IDataBaseLoader>().SingleInstance();

            builder.RegisterType<HistoryServices>()
                .As<IHistoryServices>().SingleInstance();

            builder.RegisterType<IsbnGenerator>()
               .As<IIsbnGenerator>().SingleInstance();
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
