using LMS.Models;
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
    }
}
