using LMS.Contracts;
using LMS.Models;
using LMS.Models.ModelsContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Services
{
    public class LoginAuthenticator : ILoginAuthenticator
    {
        private IUser currentUser;
        private readonly IUsersDataBase _usersDb;
        private readonly IAdminsDataBase _adminsDb;
        private readonly IValidator _validator;
        private string currentPassword;
        private string currentUsername;
        public LoginAuthenticator(IUsersDataBase usersDb,
                                  IAdminsDataBase adminsDb,
                                  IValidator validator)
        {
            _usersDb = usersDb;
            _adminsDb = adminsDb;
            _validator = validator;
        }
        public IUser GetCurrentUser()
        {
            return currentUser;
        }
        public string GetCurrentUserName()
        {
            return currentUsername;
        }
        public void SetCurrentUser(IUser _currentUser, string userName, string password)
        {
            currentPassword = password;
            currentUser = _currentUser;
            currentUsername = userName;
        }
        public IUser CheckUserCredetials(string username, string password)
        {
            var user = _usersDb.CheckUserCredetials(username, password);
            return user;
        }
        public IUser CheckAdminCredetials(string username, string password)
        {
            var admin = _adminsDb.CheckAdminCredentials(username, password);
            return admin;
        }
        public bool CheckUsernameInAdminDb(string username)
        {
            var admin = _adminsDb.CheckUsernameInAdminDb(username);
            if (admin != null)
                return true;
            return false;
        }
        public bool CheckUsernameInUserDb(string username)
        {
            var user = _usersDb.CheckUsernameInUserDb(username);
            if (user != null)
                return true;
            return false;
        }
        public bool CheckCurrentUserStatus()
        {
            var userToTest = GetCurrentUser();
            bool result = _adminsDb.CheckIUserInAdminDb(userToTest);
            if (result == true)
                return true;
            return false;
        }
        public void IsAlreadyLoggedIn()
        {
            var currUsr = GetCurrentUser();
            if (currUsr != null)
            {
                throw new ArgumentException("You are already LoggedIn!");
            }
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
            if (CheckCurrentUserStatus())
            {
                _adminsDb.RemoveAdminFromDb(userName);
            }
            _usersDb.RemoveUserFromDb(userName);
        }
        public bool CheckAllowedCommands(string consoleInput)
        {
            var currentUser = GetCurrentUser();
            if (_validator.IsNull(currentUser)
                && !_validator.CommandNameIsLogin(consoleInput)
                && !_validator.CommandNameIsRegister(consoleInput))
                return false;
            return true;
        }
    }
}