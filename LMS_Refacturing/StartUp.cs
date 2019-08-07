using Autofac;
using LMS.Core.Contracts;
using System;

namespace LMS.Start
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var builder = new AutofacContainer();

            var container = builder.RegisterContainer();

            var engine = container.Resolve<IEngine>();

            engine.Run();
        }
    }
}
