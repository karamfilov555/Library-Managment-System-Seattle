using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data.Models;

namespace LMS.Services.Contracts
{
    public interface ILoginAuthenticator
    {
        User LoggedUser();
        void SetCurrentUser(User _currentUser, string username, string password);
        User CheckUserCredetials(string username, string password);
        User CheckAdminCredetials(string username, string password);
        bool CheckCurrentUserStatus();
        void IsAdmin();
        bool CheckUsernameInAdminDb(string username);
        bool CheckUsernameInUserDb(string username);
        bool IsPasswordCorrect(string pass);
        void LogoutCurrentUser();
        void RemoveUserFromDb(string userName);
        string GetCurrentUserName();
        void IsAlreadyLoggedIn();
        void CheckAllowedCommands(string consoleInput);
    }
}
