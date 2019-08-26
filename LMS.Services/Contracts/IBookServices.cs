using LMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.Contracts
{
    public interface IBookServices
    {
        void AddBookToDb(Book book);
    }
}
