using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts.DataBaseContracts
{
    public interface IBookDataBase
    {
        IList<Book> ReadBooks();
        void WriteBooks(string jsonToOutput);
        void AddBookToJsonDB(string title, string author, int pages, int year, string country, string language, string subject, string isbn);
        void RemoveBookFromJsonDb(string title);
    }
}
