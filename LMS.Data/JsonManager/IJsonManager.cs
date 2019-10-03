using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.JsonManager
{
    public interface IJsonManager
    {
        List<T> ExtractTypesFromJson<T>(string directory);
    }
}
