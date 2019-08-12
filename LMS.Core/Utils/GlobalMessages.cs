using LMS.Contracts;
using LMS.Core.Contracts;
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
        private const string Delimiter = "=======>  Book # {0}  <=======";
        private const string WichBookYouWantToReturn = "Please, type the ISBN of book that you want to return: ";
        private const string SuccessfullyReturnBook = "You successfully return the book: " +
            "\"{0}\"";
        private const string SayWelcome = "WELCOME, into our Library!{Environment.NewLine}For better expirience, Please Login or Register in to the System!";
        private const string Statistics = "The Engine worked for {0} seconds.";
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
            return $"{Environment.NewLine}" +
                   $"==================================={Environment.NewLine}" +
                   $"=======>  New Book Added!  <======={Environment.NewLine}" +
                   $"==================================={Environment.NewLine}";
        }
        public string PrintCheckOutBookLabel()
        {
            return $"{Environment.NewLine}"+
                   $"==================================={Environment.NewLine}" +
                   $"=======> Book checked out! <======={Environment.NewLine}" +
                   $"==================================={Environment.NewLine}";
        }
        public string CatalogDelimiter(int counter)
        {
            return string.Format(Delimiter, counter);
        }
        public string BookCheckedOutMessage(string bookInfo)
        {
            return PrintCheckOutBookLabel() + $"{Environment.NewLine}" + bookInfo;
        }
        public string WichBookYouWantToReturnMessage()
        {
            return WichBookYouWantToReturn;
        }
        public string SuccessfullyReturnBookMessage(string title)
        {
            return string.Format(SuccessfullyReturnBook,title);
        }
        public string WelcomeMessage()
        {
            return SayWelcome;
        }
        public string GetTimeStatisticsMessage()
        {
            return Statistics;
        }
    }
}
