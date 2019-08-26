using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Models.ModelsFactory
{
    public interface IModelsFactory
    {
        User CreateUser(string username, string password, string roleName);
        Book CreateBook(string title, string authorName, int pages, int year, string country, string language, string subjectName);
    }
}
