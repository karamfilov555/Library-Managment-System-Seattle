using LMS.Contracts.DataBaseContracts;
using LMS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LMS.JsonDB
{
    public class UserDataBase : IUserDataBase
    {
        private const string userPath = @"../../../User.json";
        public UserDataBase()
        {
        }
        public IList<User> ReadUsers()
        {
            string jsonIn = File.ReadAllText(userPath);
            var existingUsers = JsonConvert.DeserializeObject<IList<User>>(jsonIn);
            return existingUsers;
        }
        public void WriteUsers(string jsonToOutput)
        {
            File.WriteAllText(userPath, jsonToOutput);
        }
        public void AddUserToJsonDB(string username, string password)
        {
            string jsonIn = File.ReadAllText(userPath);
            var array = JArray.Parse(jsonIn);
            var userToAdd = new JObject();
            userToAdd["Username"] = username;
            userToAdd["Password"] = password;
            array.Add(userToAdd);
            var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(userPath, jsonToOutput);
        }
        public void RemoveUserFromJsonDb(string userName)
        {
            var existingUsers = ReadUsers();
            var user = existingUsers.FirstOrDefault(x => x.Username == userName);
            existingUsers.Remove(user);
            var jsonToOutput = JsonConvert.SerializeObject(existingUsers, Formatting.Indented);
            WriteUsers(jsonToOutput);
        }
    }
}
