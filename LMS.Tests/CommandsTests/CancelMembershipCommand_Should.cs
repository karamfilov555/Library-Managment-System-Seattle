using LMS.Contracts;
using LMS.Core.Commands;
using LMS.Models.ModelsContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.CommandsTests
{
    [TestClass]
    public class CancelMembershipCommand_Should
    {
        [TestMethod]
        public void ReturnWrongPasswordMessage_WhenInvalidPasswordPass()
        {
            //Arrange
            IList<string> parameters = new List<string> {"wrongPass"};
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var loginAuthenticator = new Mock<ILoginAuthenticator>();
            validator.Setup(v => v.CancelMembershipCountValidation(parameters));
            messages.Setup(m => m.WrongPasswordMessage()).Returns("Wrong password!");
            var sut = new CancelMembershipCommand(validator.Object,messages.Object,loginAuthenticator.Object);
            //Act
             var actual = sut.Execute(parameters);
            //Assert
            Assert.AreEqual("Wrong password!", actual);
        }
        [TestMethod]
        public void CallsRemoveUserFromDbMethod_WithCurrentUser_WhenValidPasswordPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "pass" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var loginAuthenticator = new Mock<ILoginAuthenticator>();
            var currentUser = new Mock<IUser>();
            loginAuthenticator.Setup(l => l.IsPasswordCorrect("pass")).Returns(true);
            validator.Setup(v => v.CancelMembershipCountValidation(parameters));
            loginAuthenticator.Setup(u => u.GetCurrentUser()).Returns(currentUser.Object);
            //Act
            var sut = new CancelMembershipCommand(validator.Object, messages.Object, loginAuthenticator.Object);
            var actual = sut.Execute(parameters);
            //Assert
            loginAuthenticator.Verify(r => r.RemoveUserFromDb(currentUser.Object.Username), Times.Once);
        }
        [TestMethod]
        public void CallsLogoutCurrentUserMethod_WhenValidPasswordPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "pass" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var loginAuthenticator = new Mock<ILoginAuthenticator>();
            var currentUser = new Mock<IUser>();
            loginAuthenticator.Setup(l => l.IsPasswordCorrect("pass")).Returns(true);
            validator.Setup(v => v.CancelMembershipCountValidation(parameters));
            loginAuthenticator.Setup(u => u.GetCurrentUser()).Returns(currentUser.Object);
            //Act
            var sut = new CancelMembershipCommand(validator.Object, messages.Object, loginAuthenticator.Object);
            var actual = sut.Execute(parameters);
            //Assert
            loginAuthenticator.Verify(r => r.LogoutCurrentUser(), Times.Once);
        }
        [TestMethod]
        public void ReturnCorrectMessage_WhenValidPasswordPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "pass" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var loginAuthenticator = new Mock<ILoginAuthenticator>();
            var currentUser = new Mock<IUser>();
            loginAuthenticator.Setup(l => l.IsPasswordCorrect("pass")).Returns(true);
            validator.Setup(v => v.CancelMembershipCountValidation(parameters));
            messages.Setup(m => m.CancelMemershipMessage()).Returns("Your membership is successfully canceled!");
            loginAuthenticator.Setup(u => u.GetCurrentUser()).Returns(currentUser.Object);
            //Act
            var sut = new CancelMembershipCommand(validator.Object, messages.Object, loginAuthenticator.Object);
            var actual = sut.Execute(parameters);
            //Assert
            Assert.AreEqual("Your membership is successfully canceled!", actual);
        }
    }
}
