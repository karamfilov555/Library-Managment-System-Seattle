using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.ModelProviders.Contracts
{
    public interface IBookFactory
    {
        Book CreateBook(string title, string authorName, int pages, int year, string country, string language, string[] subjects);
    }
}
