using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IBookServices
    {
        void AddBookToDb(Book book);
        Book FindBook(string title, string author);
    }
}
