using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IBooksDataBase
    {
        void LoadBooksFromJson();
        void AddBookToDb(Book book);
    }
}
