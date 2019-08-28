using LMS.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;
using LMS.Services.Validator;

namespace LMS.Services
{
    public class LoginAuthenticator : ILoginAuthenticator
    {
        private User currentUser;
        private readonly IUserServices _usersServices;
        private readonly IServicesValidator _validator;
        private string currentPassword;
        private string currentUsername;
        public LoginAuthenticator(IUserServices usersServices,
                                  IServicesValidator validator)
        {
            _usersServices = usersServices;
            _validator = validator;
        }
        public User LoggedUser()
        {
            return currentUser;
        }
        public string GetCurrentUserName()
        {
            return currentUsername;
        }
        public void SetCurrentUser(User _currentUser, string username, string password)
        {
            currentPassword = password;
            currentUser = _currentUser;
            currentUsername = username;
        }
        public User CheckUserCredetials(string username, string password)
        {
            var user = _usersServices.CheckUserCredetials(username, password);
            return user;
        }
        //public bool CheckUsernameInDb(string username)
        //{
        //    var user = _usersServices.CheckUsernameInDb(username);
        //    if (user != null)
        //        return true;
        //    return false;
        //}
       
        public void IsAlreadyLoggedIn()
        {
            var currUsr = LoggedUser();
            if (currUsr != null)
            {
                throw new ArgumentException("You are already LoggedIn!");
            }
        }
        public void IsAdmin()
        {
            var currentUser = LoggedUser();
            if (currentUser.RoleId != 1) // RoleId 1 ==> is Admin
                throw new ArgumentException("You have no admin rights!");
        }
        public void IsPasswordCorrect(string pass)
        {
            var user = LoggedUser();
            if (pass != user.Password)
                throw new ArgumentException("Wrong credentials!");
        }

        public void LogoutCurrentUser()
        {
            SetCurrentUser(null, null, null);
        }

        //public void RemoveUserFromDb(string userName)
        //{
        //    if (CheckCurrentUserStatus())
        //    {
        //        _adminServices.RemoveAdminFromDb(userName);
        //    }
        //    _usersServices.RemoveUserFromDb(userName);
        //}
        public void CheckAllowedCommands(string consoleInput)
        {
            var currentUser = LoggedUser();
            if (_validator.IsNull(currentUser)
                && (!_validator.CommandNameIsLogin(consoleInput)
                && !_validator.CommandNameIsRegister(consoleInput)))
                throw new ArgumentException("For better expirience, plese LogIn or Register into the System...");
        }
    }
}
