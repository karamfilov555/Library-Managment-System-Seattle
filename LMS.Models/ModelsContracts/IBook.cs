using LMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.ModelsContracts
{
    public interface IBook
    {
        string Title { get; }
        SubjectCategory Subject { get; }
        string Author { get; }
        int Pages { get; }
        int Year { get; }
        string Country { get; }
        string Language { get; }
        string ToString();

    }
}
