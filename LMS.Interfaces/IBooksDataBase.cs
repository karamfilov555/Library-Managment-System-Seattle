using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IBooksDataBase
    {
        void LoadBooksFromJson();
        
        void AddBookToDb(IBook book);
        void RemoveFromDb(IBook book);
        IBook FindBookInDb(string title);
        string AllExistingBooksToString();
    }
}
