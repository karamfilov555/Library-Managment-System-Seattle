using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IBookServices
    {
        void LoadBooksFromJson();
        void AddBookToDb(IBook book);
        void RemoveFromDb(IBook book);
        IBook FindBookInDb(string title);
        string AllExistingBooksToString();
        string ShowAllBooksWithThisTitle(string title);
        string ShowAllBooksWithThisYear(int year);
        string ShowAllBooksWithThisAuthor(string author);
        string ShowAllBooksWithThisLanguage(string language);
        string ShowAllBooksWithThisSubject(string subject);
        int GetCopiesCount(string bookTitle);

    }
}
