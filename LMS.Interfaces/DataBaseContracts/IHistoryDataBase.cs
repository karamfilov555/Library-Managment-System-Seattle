using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts.DataBaseContracts
{
    public interface IHistoryDataBase
    {
        IList<HistoryRegistry> ReadCheckOutHistory();
        void WriteCheckOutHistory(string jsonToOutput);
        void AddToCheckOutHistoryJson(string title, string author, int pages, int year, string country, string language, string subject, string isbn, string username, string returnDate);
        void RemoveRegistryFromJsonDb(string isbn);
    }
}
