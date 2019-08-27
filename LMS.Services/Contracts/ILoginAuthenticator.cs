using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;

namespace LMS.Services.Contracts
{
    public interface ILoginAuthenticator
    {
        User LoggedUser();
        string GetCurrentUserName();
        void SetCurrentUser(User _currentUser, string username, string password);
        User CheckUserCredetials(string username, string password);
        void IsAlreadyLoggedIn();
        void IsAdmin();
        void IsPasswordCorrect(string pass);
        void LogoutCurrentUser();
        void CheckAllowedCommands(string consoleInput);
    }
}
