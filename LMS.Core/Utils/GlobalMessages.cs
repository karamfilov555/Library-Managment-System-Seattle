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
        private const string SuccessfullyLogin = "Successfully Login! Hello, ";
        private const string LogOut = "You are successfully logged out. Hope to see you soon!";
        private const string NewUserCreated = "Created new Member with username: ";
        private const string UserWithThisNameAlreadyExist = "User with this username already exist!";
        private const string InvalidParameters = "The parameters are not valid for this operation!";
        private const string SuccessfullyAddedBook = "Book has been successfully created!";
        private const string CancelMemershipPasswordRequired = "If you want to cancel your membership, Please enter your password!";
        private const string CancelMemership = "Your membership is successfully canceled!";
        private const string WrongPassword = "You have enter wrong password!";
        private const string BookRemoved = "Book with title: \"{0}\" was successfully removed";
        public string PleaseLoginOrRegisterMessage()
        {
            return PleaseLogOrReg;
        }
        public string WrongCredentialsMessage()
        {
            return WrongCredentials;
        }
        public string SuccessfullyLoginMessage(string username)
        {
            return SuccessfullyLogin + username;
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
        public string CancelMemership_PasswordRequiredMessage()
        {
            return CancelMemershipPasswordRequired;
        }
        public string CancelMemershipMessage()
        {
            return CancelMemership;
        }
        public string WrongPasswordMessage()
        {
            return WrongPassword;
        }
        public string BookRemovedMessage(string title)
        {
            return string.Format(BookRemoved,title);
        }
        public string PrintAddBookLabel()
        {
            return $"==================================={Environment.NewLine}" +
                   $"=======>  New Book Added!  <======={Environment.NewLine}" +
                   $"==================================={Environment.NewLine}";
        }
    }
}
