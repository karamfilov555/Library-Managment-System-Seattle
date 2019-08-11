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
        private readonly IUsersServices _usersServices;
        private readonly IAdminServices _adminServices;
        private readonly IValidator _validator;
        private string currentPassword;
        private string currentUsername;
        public LoginAuthenticator(IUsersServices usersServices,
                                  IAdminServices adminServices,
                                  IValidator validator)
        {
            _usersServices = usersServices;
            _adminServices = adminServices;
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
            var user = _usersServices.CheckUserCredetials(username, password);
            return user;
        }
        public IUser CheckAdminCredetials(string username, string password)
        {
            var admin = _adminServices.CheckAdminCredentials(username, password);
            return admin;
        }
        public bool CheckUsernameInAdminDb(string username)
        {
            var admin = _adminServices.CheckUsernameInAdminDb(username);
            if (admin != null)
                return true;
            return false;
        }
        public bool CheckUsernameInUserDb(string username)
        {
            var user = _usersServices.CheckUsernameInUserDb(username);
            if (user != null)
                return true;
            return false;
        }
        public bool CheckCurrentUserStatus()
        {
            var userToTest = GetCurrentUser();
            bool result = _adminServices.CheckIUserInAdminDb(userToTest);
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
                _adminServices.RemoveAdminFromDb(userName);
            }
            _usersServices.RemoveUserFromDb(userName);
        }
        public void CheckAllowedCommands(string consoleInput)
        {
            var currentUser = GetCurrentUser();
            if (_validator.IsNull(currentUser)
                && (!_validator.CommandNameIsLogin(consoleInput)
                && !_validator.CommandNameIsRegister(consoleInput)))
                throw new ArgumentException("For better expirience, plese LogIn or Register into the System...");
        }
    }
}