using LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts.DataBaseContracts
{
    public interface IHistoryDataBase
    {
        IList<HistoryRegistry> ReadCheckOutHistory();
        void WriteCheckOutHistory(string jsonToOutput);
        void AddToCheckOutHistoryJson(string title, string isbn, string username, string returnDate);
    }
}
