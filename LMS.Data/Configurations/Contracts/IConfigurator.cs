using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Configurations.Contracts
{
    public interface IConfigurator
    {
        string ProvideConnectionString();
    }
}
