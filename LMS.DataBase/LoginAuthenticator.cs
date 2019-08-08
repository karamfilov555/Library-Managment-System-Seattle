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
        public LoginAuthenticator(IUsersDataBase usersDb, IAdminsDataBase adminsDb)
        {
            this._usersDb = usersDb;
            this._adminsDb = adminsDb;
        }
        public IUser GetCurrentUser()
        {
            return currentUser;
        }
        public void SetCurrentUser(IUser _currentUser)
        {
            this.currentUser = _currentUser;
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
        public User CheckUsernameInAdminDb(string username)
        {
            var admin = this._adminsDb.CheckUsernameInAdminDb(username);
            return admin;
        }
        public User CheckUsernameInUserDb(string username)
        {
            var user = this._usersDb.CheckUsernameInUserDb(username);
            return user;
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
    }
}