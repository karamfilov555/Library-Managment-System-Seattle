using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IModelsFactory
    {
        IUser CreateUser(string username, string password);
        IBook CreateBook(string title, string author, int pages, int year, string county, string language, string subject, string isbn);
        IHistoryRegistry CreateRegistry(string title, string author, int pages, int year, string county, string language, string subject, string isbn);
    }
}
