using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LMS.JasonDB
{
    public class Json:IJson
    {
        public Json()
        {
        }
        public IList<Book> ReadBooks()
        {
            string jsonIn = File.ReadAllText(@"../../../Books.json");
            var existingBooks = JsonConvert.DeserializeObject<IList<Book>>(jsonIn);
            return existingBooks;
        }
        public void WriteBooks(string jsonToOutput)
        {
            File.WriteAllText(@"../../../Books.json", jsonToOutput);
        }
        public IList<User> ReadAdmins()
        {
            string jsonIn = File.ReadAllText(@"../../../Admin.json");
            var existingAdmins = JsonConvert.DeserializeObject<IList<User>>(jsonIn);
            return existingAdmins;
        }
        public void WriteAdmins(string jsonToOutput)
        {
            File.WriteAllText(@"../../../Admin.json", jsonToOutput);
        }
        public IList<User> ReadUsers()
        {
            string jsonIn = File.ReadAllText(@"../../../User.json");
            var existingUsers = JsonConvert.DeserializeObject<IList<User>>(jsonIn);
            return existingUsers;
        }
        public void WriteUsers(string jsonToOutput)
        {
            File.WriteAllText(@"../../../User.json", jsonToOutput);
        }
    }
}
