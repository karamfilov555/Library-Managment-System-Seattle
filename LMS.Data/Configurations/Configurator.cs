
using System.IO;

namespace LMS.Data.Configurations
{
    public class Configurator 
    {
        private const string CsFileDirectory =
        @"..\..\..\..\LMS.Data\Configurations\ConnectionStringFile\ConnectionString.txt";
        public Configurator()
        {
        }
        public static string ProvideConnectionString()
        {
            return File.ReadAllText(CsFileDirectory); 
        }
    }
}


