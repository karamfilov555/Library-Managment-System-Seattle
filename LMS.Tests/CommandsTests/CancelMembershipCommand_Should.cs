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
            var validatorMocked = new Mock<IValidator>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            validatorMocked.Setup(v => v.CancelMembershipCountValidation(parameters));
            messagesMocked.Setup(m => m.WrongPasswordMessage()).Returns("Wrong password!");
            var sut = new CancelMembershipCommand(validatorMocked.Object, messagesMocked.Object, loginAuthenticatorMocked.Object);
            //Act
             var actual = sut.Execute(parameters);
            //Assert
            Assert.AreEqual("Wrong password!", actual);
        }
        [TestMethod]
        public void Invoke_RemoveUserFromDbMethod_WithCurrentUser_WhenValidPasswordPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "pass" };
            var validatorMocked = new Mock<IValidator>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var currentUserMocked = new Mock<IUser>();
            loginAuthenticatorMocked.Setup(l => l.IsPasswordCorrect("pass")).Returns(true);
            validatorMocked.Setup(v => v.CancelMembershipCountValidation(parameters));
            loginAuthenticatorMocked.Setup(u => u.GetCurrentUser()).Returns(currentUserMocked.Object);
            //Act
            var sut = new CancelMembershipCommand(validatorMocked.Object, messagesMocked.Object, loginAuthenticatorMocked.Object);
            var actual = sut.Execute(parameters);
            //Assert
            loginAuthenticatorMocked.Verify(r => r.RemoveUserFromDb(currentUserMocked.Object.Username), Times.Once);
        }
        [TestMethod]
        public void Invoke_LogoutCurrentUserMethod_WhenValidPasswordPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "pass" };
            var validatorMocked = new Mock<IValidator>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var currentUserMocked = new Mock<IUser>();
            loginAuthenticatorMocked.Setup(l => l.IsPasswordCorrect("pass")).Returns(true);
            validatorMocked.Setup(v => v.CancelMembershipCountValidation(parameters));
            loginAuthenticatorMocked.Setup(u => u.GetCurrentUser()).Returns(currentUserMocked.Object);
            //Act
            var sut = new CancelMembershipCommand(validatorMocked.Object, messagesMocked.Object, loginAuthenticatorMocked.Object);
            var actual = sut.Execute(parameters);
            //Assert
            loginAuthenticatorMocked.Verify(r => r.LogoutCurrentUser(), Times.Once);
        }
        [TestMethod]
        public void ReturnCorrectMessage_WhenValidPasswordPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "pass" };
            var validatorMocked = new Mock<IValidator>();
            var messagesMocked = new Mock<IGlobalMessages>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var currentUserMocked = new Mock<IUser>();
            loginAuthenticatorMocked.Setup(l => l.IsPasswordCorrect("pass")).Returns(true);
            validatorMocked.Setup(v => v.CancelMembershipCountValidation(parameters));
            messagesMocked.Setup(m => m.CancelMemershipMessage()).Returns("Your membership is successfully canceled!");
            loginAuthenticatorMocked.Setup(u => u.GetCurrentUser()).Returns(currentUserMocked.Object);
            //Act
            var sut = new CancelMembershipCommand(validatorMocked.Object, messagesMocked.Object, loginAuthenticatorMocked.Object);
            var actual = sut.Execute(parameters);
            //Assert
            Assert.AreEqual("Your membership is successfully canceled!", actual);
        }
    }
}
