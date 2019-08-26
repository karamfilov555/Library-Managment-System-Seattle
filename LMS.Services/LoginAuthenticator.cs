using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.Data.Models;

namespace LMS.Services
{
    public class LoginAuthenticator : ILoginAuthenticator
    {
        //TODO
        public User LoggedUser()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentUser(User _currentUser, string username, string password)
        {
            throw new NotImplementedException();
        }

        public User CheckUserCredetials(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User CheckAdminCredetials(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckCurrentUserStatus()
        {
            throw new NotImplementedException();
        }

        public void IsAdmin()
        {
            throw new NotImplementedException();
        }

        public bool CheckUsernameInAdminDb(string username)
        {
            throw new NotImplementedException();
        }

        public bool CheckUsernameInUserDb(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsPasswordCorrect(string pass)
        {
            throw new NotImplementedException();
        }

        public void LogoutCurrentUser()
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromDb(string userName)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserName()
        {
            throw new NotImplementedException();
        }

        public void IsAlreadyLoggedIn()
        {
            throw new NotImplementedException();
        }

        public void CheckAllowedCommands(string consoleInput)
        {
            throw new NotImplementedException();
        }
    }
}
