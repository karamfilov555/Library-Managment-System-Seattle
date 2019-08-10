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
        string RegistryInfo();
    }
}
