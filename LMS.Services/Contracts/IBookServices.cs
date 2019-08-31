using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IBookServices
    {
        void AddBookToDb(Book book);
        Book FindBook(string title);
        Book FindBook(string title, string author);
        void SetReserveBookStatus(Book book);
        void SetCheckOutBookStatus(Book book);
        void SetReturnBookStatus(Book book);
        void CheckIfBookExist(string title, string author);
        bool CheckIfBookExist(string title);
        Book FindAvailableBook(string title, string author);
        string AllBooksToString();
        string AllBooksToString(IList<Book> books);
        IList<Book> SearchByAuthor(string authorName);
    }
}
