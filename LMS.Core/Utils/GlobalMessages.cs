using LMS.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Core.Utils
{
    public class GlobalMessages : IGlobalMessages
    {
        private const string PleaseLogOrReg = "Please, Login or Register";
        private const string WrongCredentials = "It doesn't exist user with those credentials. Notice that the login form is case-sensitive!";
        private const string SuccessfullyLogin = "Successfully Login!";
        private const string LogOut = "You are successfully logged out. Hope to see you soon!";
        private const string NewUserCreated = "Created new Member with username: ";
        private const string UserWithThisNameAlreadyExist = "User with this username already exist!";
        private const string InvalidParameters = "The parameters are not valid for this operation!";
        private const string SuccessfullyAddedBook = "Book has been successfully created!";
        public string PleaseLoginOrRegisterMessage()
        {
            return PleaseLogOrReg;
        }
        public string WrongCredentialsMessage()
        {
            return WrongCredentials;
        }
        public string SuccessfullyLoginMessage()
        {
            return SuccessfullyLogin;
        }
        public string LogOutMessage()
        {
            return LogOut;
        }
        public string RegisterMessage(string username)
        {
            return NewUserCreated + username;
        }
        public string ThisUserAlreadyExistMessage()
        {
            return UserWithThisNameAlreadyExist;
        }
        public string InvalidParametersMessage()
        {
            return InvalidParameters;
        }
        public string BookCreated()
        {
            return SuccessfullyAddedBook;
        }
    }
}
