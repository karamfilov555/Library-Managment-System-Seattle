using LMS.Contracts;
using LMS.Contracts.DataBaseContracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services
{
    public class HistoryServices : IHistoryServices
    {
        private readonly IHistoryDataBase _historyDataBase;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private IList<IHistoryRegistry> history = new List<IHistoryRegistry>();
        public HistoryServices(IHistoryDataBase historyDataBase, 
                               ILoginAuthenticator loginAuthenticator)
        {
            _historyDataBase = historyDataBase;
            _loginAuthenticator = loginAuthenticator;
        }
        
        public void AddRegistryToHistoryDb(IHistoryRegistry registry)
        {
            history.Add(registry);

            _historyDataBase.AddToCheckOutHistoryJson(registry.Title,registry.Author,registry.Pages,registry.Year, registry.Country,
               registry.Language,registry.Subject.ToString(), registry.ISBN, registry.Username,registry.ReturnDate);
        }
        public void LoadHistoryFromJson()
        {
            var existingHistory = _historyDataBase.ReadCheckOutHistory();

            foreach (var registry in existingHistory)
            {
                history.Add(registry);
            }
        }
        public void CheckBooksOfCurrentUser()
        {
            var currentUsername = _loginAuthenticator.GetCurrentUserName();

            var repeat = history.Where(x => x.Username == currentUsername).ToList();
            if (repeat.Count >= 5)
                throw new ArgumentException("You have reached the maximum amount of 5 checked-out books! If u want to check-out this item, you have to return book!");
        }
        public string GetHistoryOfCurrentUser()
        {
            var currentUser = _loginAuthenticator.GetCurrentUserName();
            var strBuilder = new StringBuilder();
            foreach (var registry in history)
            {
                if (registry.Username == currentUser)
                {
                    strBuilder.AppendLine(registry.RegistryInfo());
                }
            }
            return 
                $"{Environment.NewLine}"+
                $"========{currentUser}, this is your Check-Out History ========" +
                $"{Environment.NewLine}" + strBuilder.ToString();
        }
        public IHistoryRegistry FindHistoryRegistry(string isbn)
        {
            var registyToFind = history.FirstOrDefault(x => x.ISBN == isbn);
            if (registyToFind == null)
                throw new ArgumentException("There are no book with this ISBN in your check-out history!");
            return registyToFind;
        }
        public void RemoveFromHistory(IHistoryRegistry registry)
        {
            history.Remove(registry);
            _historyDataBase.RemoveRegistryFromJsonDb(registry.ISBN);
        }
    }
}
