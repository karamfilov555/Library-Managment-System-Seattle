﻿using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.JasonDB.Contracts
{
    public interface IJson
    {

        IList<Book> ReadBooks();
        void WriteBooks(string jsonToOutput);
        IList<User> ReadAdmins();
        void WriteAdmins(string jsonToOutput);
        IList<User> ReadUsers();
        void WriteUsers(string jsonToOutput);
        void AddUserToJsonDB(string username, string password);
        void AddBookToJsonDB(string title, string author, int pages, int year, string country, string language, string subject);
        void RemoveUserFromJsonDb(string userName);
    }
}