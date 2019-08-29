using LMS.Services.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LMS.Services
{
    public class JsonServices : IJsonServices
    {
        public JsonServices()
        {

        }
        public List<T> ExtractTypesFromJson<T>(string directory)
        {
            var jsonToExtractFrom = File.ReadAllText(directory);
            var objects = JsonConvert.DeserializeObject<T[]>(jsonToExtractFrom);
            var result = new List<T>();
            foreach (var item in objects)
                result.Add(item);
            return result;
        }
    }
}
