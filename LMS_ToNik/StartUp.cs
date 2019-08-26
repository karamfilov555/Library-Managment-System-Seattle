using Autofac;
using LMS.Core.Contracts;
using LMS.Start;
using System;

namespace LMS_ToNik
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var builder = new AutofacContainer();

            var container = builder.RegisterContainer();

            var decoratorEngine = container.Resolve<IDecoratorEngine>();

            decoratorEngine.Start();
        }
    }
}
