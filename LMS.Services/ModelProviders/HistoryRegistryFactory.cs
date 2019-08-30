using LMS.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services.ModelProviders
{
    public class HistoryRegistryFactory : IHistoryRegistryFactory
    {
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IBookServices _bookServices;

        public HistoryRegistryFactory(ILoginAuthenticator loginAuthenticator, IBookServices bookServices)
        {
            _loginAuthenticator = loginAuthenticator;
            _bookServices = bookServices;
        }

        public HistoryRegistry CreateHistoryRegistry(string title, string author)
        {
            var user = _loginAuthenticator.LoggedUser();
            var book = _bookServices.FindAvailableBook(title, author);
            _bookServices.SetCheckOutBookStatus(book);
            var checkOut = new HistoryRegistry(user, book);
            return checkOut;
        }
    }
}
