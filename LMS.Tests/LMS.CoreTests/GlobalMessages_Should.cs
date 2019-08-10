using LMS.Core.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.LMS.CoreTests
{
    [TestClass]
    public class GlobalMessages_Should
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

        [TestMethod]
        public void Constructor_ShouldMakeInstanceOfGlobalMsg()
        {
            var sut = new GlobalMessages();
            Assert.IsInstanceOfType(sut, typeof(GlobalMessages));
        }
        [TestMethod]
        public void PleaseLoginOrRegisterMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.PleaseLoginOrRegisterMessage();
            Assert.AreEqual(actual, PleaseLogOrReg );
        }
        [TestMethod]
        public void WrongCredentialsMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.WrongCredentialsMessage();
            Assert.AreEqual(actual, WrongCredentials);
        }
        [TestMethod]
        public void SuccessfullyLoginMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.SuccessfullyLoginMessage("cool");
            Assert.AreEqual(actual, SuccessfullyLogin+"cool");
        }
        [TestMethod]
        public void LogOutMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.LogOutMessage();
            Assert.AreEqual(actual, LogOut);
        }
        [TestMethod]
        public void RegisterMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.RegisterMessage("cool");
            Assert.AreEqual(actual, NewUserCreated+"cool");
        }
        [TestMethod]
        public void ThisUserAlreadyExistMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.ThisUserAlreadyExistMessage();
            Assert.AreEqual(actual, UserWithThisNameAlreadyExist);
        }
        [TestMethod]
        public void InvalidParametersMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.InvalidParametersMessage();
            Assert.AreEqual(actual, InvalidParameters);
        }
        [TestMethod]
        public void BookCreatedMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.BookCreated();
            Assert.AreEqual(actual, SuccessfullyAddedBook);
        }
        [TestMethod]
        public void CancelMemershipMessageMethod_PasswordRequiredMessage_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.CancelMemership_PasswordRequiredMessage();
            Assert.AreEqual(actual, CancelMemershipPasswordRequired);
        }
        [TestMethod]
        public void CancelMemershipMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.CancelMemershipMessage();
            Assert.AreEqual(actual, CancelMemership);
        }
        [TestMethod]
        public void WrongPasswordMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.WrongPasswordMessage();
            Assert.AreEqual(actual, WrongPassword);
        }
        [TestMethod]
        public void BookRemovedMessageMethod_ShouldReturnCorrectMessage()
        {
            var sut = new GlobalMessages();
            var actual = sut.BookRemovedMessage("title");
            Assert.AreEqual(actual, string.Format(BookRemoved, "title"));
        }
        
    }
}
