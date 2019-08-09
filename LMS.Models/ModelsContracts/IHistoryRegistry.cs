using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.ModelsContracts
{
    public interface IHistoryRegistry
    {
        string Title { get; }
        string ISBN { get; }
        string Username { get; }
        string ReturnDate { get; }
        string RegistryInfo();
    }
}
