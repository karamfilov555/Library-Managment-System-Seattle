using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Contracts
{
    public interface ILoginAuthenticator
    {
        IUser GetCurrentUser();
        void SetCurrentUser(IUser _currentUser,string username, string password);
        IUser CheckUserCredetials(string username, string password);
        IUser CheckAdminCredetials(string username, string password);
        bool CheckCurrentUserStatus();
        void IsAdmin();
        bool CheckUsernameInAdminDb(string username);
        bool CheckUsernameInUserDb(string username);
        bool IsPasswordCorrect(string pass);
        void LogoutCurrentUser();
        void RemoveUserFromDb(string userName);
        string GetCurrentUserName();
        void IsAlreadyLoggedIn();
    }
}
