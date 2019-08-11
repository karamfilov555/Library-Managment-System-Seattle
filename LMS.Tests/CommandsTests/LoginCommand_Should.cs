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
    public class LoginCommand_Should
    {
        [TestMethod]
        public void Invoke_IsAlreadyLoginMethod()
        {
            //Arrange
            IList<string> parameters = new List<string> { "username","password" };
            var validatorMocked = new Mock<IValidator>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var messegesMocked = new Mock<IGlobalMessages>();
            loginAuthenticatorMocked.Setup(c => c.IsAlreadyLoggedIn());
            validatorMocked.Setup(l => l.LoginParametersCountValidation(parameters));
            //Act
            var sut = new LoginCommand(validatorMocked.Object, loginAuthenticatorMocked.Object,messegesMocked.Object);
            sut.Execute(parameters);
            //Assert
            loginAuthenticatorMocked.Verify(check => check.IsAlreadyLoggedIn(), Times.Once);
        }
        [TestMethod]
        public void Invoke_CheckUserCredentialsMethod()
        {
            //Arrange
            IList<string> parameters = new List<string> { "username", "password" };
            var validatorMocked = new Mock<IValidator>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var messegesMocked = new Mock<IGlobalMessages>();
            loginAuthenticatorMocked.Setup(c => c.IsAlreadyLoggedIn());
            var user = loginAuthenticatorMocked.Setup(cr => cr.CheckUserCredetials("username", "password"));
            validatorMocked.Setup(l => l.LoginParametersCountValidation(parameters));
            //Act
            var sut = new LoginCommand(validatorMocked.Object, loginAuthenticatorMocked.Object, messegesMocked.Object);
            sut.Execute(parameters);
            //Assert
            loginAuthenticatorMocked.Verify(check => check.CheckUserCredetials("username", "password"), Times.Once);
        }
        [TestMethod]
        public void Invoke_SetCurrentUser_IfUserCredentialsPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "username", "password" };
            var validatorMocked = new Mock<IValidator>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var messegesMocked = new Mock<IGlobalMessages>();
            var userMocked = new Mock<IUser>();
            userMocked.Setup(u => u.Username).Returns("username");
            userMocked.Setup(p => p.Password).Returns("password");
            loginAuthenticatorMocked.Setup(c => c.IsAlreadyLoggedIn());
            var user = loginAuthenticatorMocked.Setup(cr => cr.CheckUserCredetials("username", "password")).Returns(userMocked.Object);
            loginAuthenticatorMocked.Setup(s => s.SetCurrentUser(userMocked.Object, "username", "password"));
            validatorMocked.Setup(l => l.LoginParametersCountValidation(parameters));
            //Act
            var sut = new LoginCommand(validatorMocked.Object, loginAuthenticatorMocked.Object, messegesMocked.Object);
            sut.Execute(parameters);
            //Assert
            loginAuthenticatorMocked.Verify(s => s.SetCurrentUser(userMocked.Object, "username", "password"), Times.Once);
        }
        [TestMethod]
        public void Invoke_SetCurrentUser_IfADMINCredentialsPassed()
        {
            //Arrange
            IList<string> parameters = new List<string> { "username", "password" };
            var validatorMocked = new Mock<IValidator>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var messegesMocked = new Mock<IGlobalMessages>();
            var userMocked = new Mock<IUser>();
            userMocked.Setup(u => u.Username).Returns("username");
            userMocked.Setup(p => p.Password).Returns("password");
            loginAuthenticatorMocked.Setup(c => c.IsAlreadyLoggedIn());
            var user = loginAuthenticatorMocked.Setup(cr => cr.CheckAdminCredetials("username", "password")).Returns(userMocked.Object);
            loginAuthenticatorMocked.Setup(s => s.SetCurrentUser(userMocked.Object, "username", "password"));
            validatorMocked.Setup(l => l.LoginParametersCountValidation(parameters));
            //Act
            var sut = new LoginCommand(validatorMocked.Object, loginAuthenticatorMocked.Object, messegesMocked.Object);
            sut.Execute(parameters);
            //Assert
            loginAuthenticatorMocked.Verify(s => s.SetCurrentUser(userMocked.Object, "username", "password"), Times.Once);
        }
        [TestMethod]
        public void Invoke_SuccessfullyLoginMessageMethod()
        {
            //Arrange
            IList<string> parameters = new List<string> { "username", "password" };
            var validatorMocked = new Mock<IValidator>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var messegesMocked = new Mock<IGlobalMessages>();
            var userMocked = new Mock<IUser>();
            userMocked.Setup(u => u.Username).Returns("username");
            userMocked.Setup(p => p.Password).Returns("password");
            loginAuthenticatorMocked.Setup(c => c.IsAlreadyLoggedIn());
            //var user = loginAuthenticatorMocked.Setup(cr => cr.CheckAdminCredetials("username", "password")).Returns(userMocked.Object);
            //loginAuthenticatorMocked.Setup(s => s.SetCurrentUser(userMocked.Object, "username", "password"));
            validatorMocked.Setup(l => l.LoginParametersCountValidation(parameters));
            messegesMocked.Setup(m => m.SuccessfullyLoginMessage("username"));
            //Act
            var sut = new LoginCommand(validatorMocked.Object, loginAuthenticatorMocked.Object, messegesMocked.Object);
            sut.Execute(parameters);
            //Assert
            messegesMocked.Verify(s => s.SuccessfullyLoginMessage("username"), Times.Once);
        }
        [TestMethod]
        public void Invoke_LoginParametersCountValidation()
        {
            //Arrange
            IList<string> parameters = new List<string> { "username" , "password"};
            var validatorMocked = new Mock<IValidator>();
            var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            var messegesMocked = new Mock<IGlobalMessages>();
            var userMocked = new Mock<IUser>();
            userMocked.Setup(u => u.Username).Returns("username");
            userMocked.Setup(p => p.Password).Returns("password");
            loginAuthenticatorMocked.Setup(c => c.IsAlreadyLoggedIn());
            var user = loginAuthenticatorMocked.Setup(cr => cr.CheckAdminCredetials("username", "password")).Returns(userMocked.Object);
            //loginAuthenticatorMocked.Setup(s => s.SetCurrentUser(userMocked.Object, "username", "password"));
            validatorMocked.Setup(l => l.IsNull(userMocked.Object)).Returns(true);
            validatorMocked.Setup(l => l.LoginParametersCountValidation(parameters));
            messegesMocked.Setup(m => m.SuccessfullyLoginMessage("username"));
            //Act
            var sut = new LoginCommand(validatorMocked.Object, loginAuthenticatorMocked.Object, messegesMocked.Object);
            sut.Execute(parameters);
            //Assert
            validatorMocked.Verify(s => s.LoginParametersCountValidation(parameters), Times.Once);
        }
        [TestMethod]
        public void Invoke_WrongCredentialsMethod_IfInvalidCredentialsPassed()
        {
            ////Arrange
            //IList<string> parameters = new List<string> { "WronGPass", "WrongUsr" };
            //var validatorMocked = new Mock<IValidator>();
            //var loginAuthenticatorMocked = new Mock<ILoginAuthenticator>();
            //var messegesMocked = new Mock<IGlobalMessages>();
            //var userMocked = new Mock<IUser>();
            
            //loginAuthenticatorMocked.Setup(c => c.IsAlreadyLoggedIn());
            //validatorMocked.Setup(c => c.IsNull(userMocked.Object)).Returns(true);
            //messegesMocked.Setup(m => m.WrongCredentialsMessage());
            //validatorMocked.Setup(l => l.LoginParametersCountValidation(parameters));
            ////Act
            //var sut = new LoginCommand(validatorMocked.Object, loginAuthenticatorMocked.Object, messegesMocked.Object);
            //sut.Execute(parameters);
            ////Assert
            //messegesMocked.Verify(s => s.WrongCredentialsMessage(), Times.Once);
        }
    }
}
