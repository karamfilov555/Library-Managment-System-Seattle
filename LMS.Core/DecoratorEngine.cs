using System.Diagnostics;
using LMS.Core.Contracts;
using LMS.Services.Contracts;

namespace LMS.Core
{
    public class DecoratorEngine : IDecoratorEngine
    {
        private readonly IDataBaseLoader _dataBaseLoader;
        private readonly IEngine _engine;
        private readonly IGlobalMessages _messages;
        private readonly ILogoPrinter _logoPrinter;
        private readonly IOutputWriter _writer;

        public DecoratorEngine(IEngine engine,
                               IDataBaseLoader dataBaseLoader,
                               IGlobalMessages messages,
                               IOutputWriter writer,
                               ILogoPrinter logoPrinter)
        {
            _dataBaseLoader = dataBaseLoader;
            _engine = engine;
            _messages = messages;
            _logoPrinter = logoPrinter;
            _writer = writer;
        }
        public void Start()
        {
            var time = new Stopwatch();
            time.Start();
            _dataBaseLoader.SeedDataBase();
            _logoPrinter.PrintLogo();
            _writer.WriteLine(_messages.WelcomeMessage());
            _engine.Run();
            time.Stop();
            var seconds = time.Elapsed.TotalSeconds;
            _writer.WriteLine(_messages.GetTimeReportMessage(seconds));
        }
    }
}
