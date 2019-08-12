using LMS.Contracts;
using LMS.Core.Contracts;
using LMS.Generators.Contracts;
using System;
using System.Diagnostics;

namespace LMS.Core.LmsDecoratorEngine
{
    public class DecoratorEngine : IDecoratorEngine
    {
        private readonly IEngine _engine;
        private readonly IGlobalMessages _messages;
        private readonly ILogoPrinter _logoPrinter;
        private readonly IOutputWriter _writer;
        private readonly IDataBaseLoader _dataBaseLoader;

        public DecoratorEngine(IEngine engine, 
                               IGlobalMessages messages, 
                               IOutputWriter writer, 
                               ILogoPrinter logoPrinter,
                               IDataBaseLoader dataBaseLoader)
        {
            _engine = engine;
            _messages = messages;
            _logoPrinter = logoPrinter;
            _writer = writer;
            _dataBaseLoader = dataBaseLoader;
        }
        public void Start()
        {
            var time = new Stopwatch();
            time.Start();
            _dataBaseLoader.FillDataBase();
            _logoPrinter.PrintLogo();
            _writer.WriteLine(_messages.WelcomeMessage());
            _engine.Run();
            time.Stop();
            var seconds = time.Elapsed.TotalSeconds;
            _writer.WriteLine(_messages.GetTimeReportMessage(seconds));
        }
    }
}
