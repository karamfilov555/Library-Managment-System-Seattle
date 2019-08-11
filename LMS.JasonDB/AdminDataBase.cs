using LMS.Contracts.DataBaseContracts;
using LMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LMS.JsonDB
{
    public class AdminDataBase : IAdminDataBase
    {
        private const string adminPath = @"../../../Admin.json";
        public AdminDataBase()
        {
        }
        public IList<User> ReadAdmins()
        {
            string jsonIn = File.ReadAllText(adminPath);
            var existingAdmins = JsonConvert.DeserializeObject<IList<User>>(jsonIn);
            return existingAdmins;
        }
        public void WriteAdmins(string jsonToOutput)
        {
            File.WriteAllText(adminPath, jsonToOutput);
        }
        public void RemoveAdminFromJsonDb(string userName)
        {
            var existingAdmins = ReadAdmins();
            var admin = existingAdmins.FirstOrDefault(x => x.Username == userName);
            existingAdmins.Remove(admin);
            var jsonToOutput = JsonConvert.SerializeObject(existingAdmins, Formatting.Indented);
            WriteAdmins(jsonToOutput);
        }
    }
}
