using LMS.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.DataBase
{
    public class LoginAuthenticator : ILoginAuthenticator
    {
        private IUser currentUser;
        private readonly IUsersDataBase _usersDb;
        private readonly IAdminsDataBase _adminsDb;
        private string currentPassword;
        private string currentUsername;
        public LoginAuthenticator(IUsersDataBase usersDb, IAdminsDataBase adminsDb)
        {
            this._usersDb = usersDb;
            this._adminsDb = adminsDb;
        }
        public IUser GetCurrentUser()
        {
            return currentUser;
        }
        public string GetCurrentUserName()
        {
            return this.currentUsername;
        }
        public void SetCurrentUser(IUser _currentUser, string userName, string password)
        {
            this.currentPassword = password;
            this.currentUser = _currentUser;
            this.currentUsername = userName;
        }
        public User CheckUserCredetials(string username, string password)
        {
            var user = this._usersDb.CheckUserCredetials(username, password);
            return user;
        }
        public User CheckAdminCredetials(string username, string password)
        {
            var admin = this._adminsDb.CheckAdminCredentials(username, password);
            return admin;
        }
        public bool CheckUsernameInAdminDb(string username)
        {
            var admin = this._adminsDb.CheckUsernameInAdminDb(username);
            if (admin != null)
                return true;
            return false;
        }
        public bool CheckUsernameInUserDb(string username)
        {
            var user = this._usersDb.CheckUsernameInUserDb(username);
            if (user != null)
                return true;
            return false;
        }
        public bool CheckCurrentUserStatus()
        {
            var userToTest = GetCurrentUser();
            bool result = this._adminsDb.CheckIUserInAdminDb(userToTest);
            if (result == true)
                return true;
            return false;
        }

        public void IsAdmin()
        {
            var check = CheckCurrentUserStatus();
            if (check == false)
            {
                throw new ArgumentException("You have no admin rights!");
            }
        }
        public bool IsPasswordCorrect(string pass)
        {
            var user = GetCurrentUser();
            if (pass == currentPassword)
            {
                return true;
            }
            return false;
        }

        public void LogoutCurrentUser()
        {
            SetCurrentUser(null, null, null);
        }

        public void RemoveUserFromDb(string userName)
        {
            //if (CheckCurrentUserStatus())
            //{
            //    this._adminsDb.RemoveUserFromDb();
            //}

            this._usersDb.RemoveUserFromDb(userName);

        }

    }
}