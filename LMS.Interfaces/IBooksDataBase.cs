using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IBooksDataBase
    {
        void LoadBooksFromJson();
        void AddBookToJsonDb(string title, string author, int pages, int year, string country, string language, string subject);
        void AddBookToDb(Book book);
    }
}
