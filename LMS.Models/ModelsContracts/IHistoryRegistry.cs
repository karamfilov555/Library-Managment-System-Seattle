using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.ModelsContracts
{
    public interface IHistoryRegistry
    {
        string Title { get; set; }
        string ISBN { get; set; }
        string Username { get; set; }
        string ReturnDate { get; set; }
        string Author { get; }
        int Pages { get; }
        int Year { get; }
        string Country { get; }
        string Language { get; }
        SubjectCategory Subject { get; }
        string RegistryInfo();
    }
}
