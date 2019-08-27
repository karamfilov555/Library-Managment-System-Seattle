using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IAuthorServices
    {
        bool CheckIfAuthorExist(string name);
        Author AddAuthorToDb(Author author);
        Author FindAuthorByName(string name);
        Author ProvideAuthor(string name);
    }
}
