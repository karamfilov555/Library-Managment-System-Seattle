
using LMS.Data.Configurations.Contracts;
using System.IO;

namespace LMS.Data.Configurations
{
    public class Configurator : IConfigurator
    {
        private const string CsFileDirectory =
        @"..\..\..\..\LMS.Data\Configurations\ConnectionStringFile\ConnectionString.txt";
        public Configurator()
        {
            ConnectionString = File.ReadAllText(CsFileDirectory);
        }
        public string ProvideConnectionString()
        {
            return ConnectionString;
        }
        private string ConnectionString { get; set; }
    }
}


