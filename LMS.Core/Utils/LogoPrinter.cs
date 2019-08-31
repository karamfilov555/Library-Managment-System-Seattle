using System.IO;
using LMS.Core.Contracts;

namespace LMS.Core.Utils
{
    public class LogoPrinter : ILogoPrinter
    {
        private readonly IOutputWriter _writer;
        private const string logoDirectory = @"..\..\..\..\LMS.Core\Utils\LogoFile\Logo.txt";
        public LogoPrinter(IOutputWriter writer)
        {
            _writer = writer;
        }
        public void PrintLogo()
        {
            var logo = File.ReadAllText(logoDirectory);
            _writer.WriteLine(logo);
        }
    }
}
