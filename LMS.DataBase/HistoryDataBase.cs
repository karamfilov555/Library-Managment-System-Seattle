using LMS.Contracts;
using LMS.JasonDB.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Services
{
    public class HistoryDataBase : IHistoryDataBase
    {
        private readonly IJson _json;
        private readonly ILoginAuthenticator _loginAuthenticator;
        private IList<IHistoryRegistry> history = new List<IHistoryRegistry>();
        public HistoryDataBase(IJson json, ILoginAuthenticator loginAuthenticator)
        {
            _json = json;
            _loginAuthenticator = loginAuthenticator;
        }
        //public void AddBookToCheckOut(IHistoryRegistry registry)
        //{
        //    this.history.Add(registry);
        //    var currentUsername = _loginAuthenticator.GetCurrentUserName();
        //    _json.AddToCheckOutHistoryJson(book.Title, book.Author, book.Pages, book.Year, book.Country, book.Language, "ISbn", currentUsername, "datata");
        //}
        public void LoadHistoryFromJson()
        {
            var existingHistory = _json.ReadCheckOutHistory();

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
    }
}
