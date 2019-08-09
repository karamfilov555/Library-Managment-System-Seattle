using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public void AddUserToJsonDB(string username, string password)
        {
            string jsonIn = File.ReadAllText(@"../../../User.json");
            var array = JArray.Parse(jsonIn);
            var userToAdd = new JObject();
            userToAdd["Username"] = username;
            userToAdd["Password"] = password;
            array.Add(userToAdd);
            var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(@"../../../User.json", jsonToOutput);
        }
        public void AddBookToJsonDB(string title, string author, int pages, int year, string country, string language, string subject)
        {
            var initialFile = File.ReadAllText(@"../../../Books.json");
            var array = JArray.Parse(initialFile);
            var bookToAdd = new JObject();
            bookToAdd["Author"] = author;
            bookToAdd["Country"] = country;
            bookToAdd["Language"] = language;
            bookToAdd["Subject"] = subject;
            bookToAdd["Pages"] = pages;
            bookToAdd["Title"] = title;
            bookToAdd["Year"] = year;
            bookToAdd["Rack"] = title[0].ToString().ToUpper();
            bookToAdd["Reservation"] = "Available";
            array.Add(bookToAdd);
            var jsonToOutput = JsonConvert.SerializeObject(array, Formatting.Indented);
            File.WriteAllText(@"../../../Books.json", jsonToOutput);
        }
        public void RemoveUserFromJsonDb(string userName)
        {
            var existingUsers = ReadUsers();
            var user = existingUsers.FirstOrDefault(x => x.Username == userName);
            existingUsers.Remove(user);
            var jsonToOutput = JsonConvert.SerializeObject(existingUsers, Formatting.Indented);
            WriteUsers(jsonToOutput);
        }
        public void RemoveAdminFromJsonDb(string userName)
        {
            var existingAdmins = ReadAdmins();
            var admin = existingAdmins.FirstOrDefault(x => x.Username == userName);
            existingAdmins.Remove(admin);
            var jsonToOutput = JsonConvert.SerializeObject(existingAdmins, Formatting.Indented);
            WriteAdmins(jsonToOutput);
        }
        public void RemoveBookFromJsonDb(string title)
        {
            var existingBooks = ReadBooks();
            var book = existingBooks.FirstOrDefault(x => x.Title == title);
            existingBooks.Remove(book);
            var jsonToOutput = JsonConvert.SerializeObject(existingBooks, Formatting.Indented);
            WriteBooks(jsonToOutput);
        }
    }
}
