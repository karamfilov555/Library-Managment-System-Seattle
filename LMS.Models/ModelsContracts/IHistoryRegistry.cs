using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.ModelsContracts
{
    public interface IHistoryRegistry
    {
        string Title { get; }
        string Subject { get; }
        string Author { get; }
        int Pages { get; }
        int Year { get; }
        string Country { get; }
        string Language { get; }
        string Username { get; }
        string ReturnDate { get; }
        
    }
}
