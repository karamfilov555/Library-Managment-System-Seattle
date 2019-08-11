using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface IHistoryServices
    {
        void CheckBooksOfCurrentUser();
        void LoadHistoryFromJson();
        void AddRegistryToHistoryDb(IHistoryRegistry registry);
        string GetHistoryOfCurrentUser();
        IHistoryRegistry FindHistoryRegistry(string isbn);
        void RemoveFromHistory(IHistoryRegistry registry);
    }
}
