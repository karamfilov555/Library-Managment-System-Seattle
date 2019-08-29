using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models.Models;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;

namespace LMS.Services.ModelProviders
{
    public class ReserveBookFactory : IReserveBookFactory
    
    {
        private readonly ILoginAuthenticator _loginAuthenticator;
        private readonly IBookServices _bookServices;

        public ReserveBookFactory(ILoginAuthenticator loginAuthenticator, IBookServices bookServices)
        {
            _loginAuthenticator = loginAuthenticator;
            _bookServices = bookServices;
        }

        public ReserveBook CreateReserveBook(string title, string author)
        {
            var user = _loginAuthenticator.LoggedUser();
            var book = _bookServices.FindBook(title, author);

            var reservation = new ReserveBook(user,book);
            return reservation;
        }
    }
}
