using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IJsonServices
    {
        List<T> ExtractTypesFromJson<T>(string directory);
    }
}
