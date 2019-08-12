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
    public class RegisterCommand_Should
    {
        [TestMethod]
        public void Invoke_CreateUserMethod()
        {
            IList<string> parameters = new List<string> { "username", "password" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            var factory = new Mock<IModelsFactory>();
            var services = new Mock<IUsersServices>();
            var user = new Mock<IUser>();
            user.Setup(u => u.Username).Returns("username");
            user.Setup(u => u.Password).Returns("password");

            login.Setup(l => l.CheckUsernameInUserDb("username")).Returns(false);
            login.Setup(l => l.CheckUsernameInAdminDb("username")).Returns(false);
            var newUser = factory.Setup(f => f.CreateUser("username", "password")).Returns(user.Object);
            var sut = new RegisterCommand(validator.Object, messages.Object, login.Object, factory.Object, services.Object);
            sut.Execute(parameters);

            factory.Verify(v => v.CreateUser("username", "password"), Times.Once);
        }
        [TestMethod]
        public void Invoke_IsAlreadyLoggedInMethod()
        {
            IList<string> parameters = new List<string> { "username", "password" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            var factory = new Mock<IModelsFactory>();
            var services = new Mock<IUsersServices>();

            var sut = new RegisterCommand(validator.Object, messages.Object, login.Object, factory.Object, services.Object);
            sut.Execute(parameters);

            login.Verify(l => l.IsAlreadyLoggedIn(), Times.Once);
        }
        [TestMethod]
        public void Invoke_RegisterParametersCountValidationMethod()
        {
            // Arrange
            IList<string> parameters = new List<string> { "username", "password" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            var factory = new Mock<IModelsFactory>();
            var services = new Mock<IUsersServices>();
            // Act
            var sut = new RegisterCommand(validator.Object, messages.Object, login.Object, factory.Object, services.Object);
            sut.Execute(parameters);
            // Verify
            validator.Verify(v => v.RegisterParametersCountValidation(parameters), Times.Once);
        }
        [TestMethod]
        public void Invoke_ThisUserAlreadyExistMethod_IfUsernamePassed_AlreadyExistIn_ADMIN_Db()
        {
            IList<string> parameters = new List<string> { "username", "password" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            var factory = new Mock<IModelsFactory>();
            var services = new Mock<IUsersServices>();

            login.Setup(l => l.CheckUsernameInAdminDb("username")).Returns(true);

            var sut = new RegisterCommand(validator.Object, messages.Object, login.Object, factory.Object, services.Object);
            sut.Execute(parameters);

            messages.Verify(v => v.ThisUserAlreadyExistMessage(), Times.Once);
        }
        [TestMethod]
        public void Invoke_ThisUserAlreadyExistMethod_IfUsernamePassed_AlreadyExistIn_USER_Db()
        {
            IList<string> parameters = new List<string> { "username", "password" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            var factory = new Mock<IModelsFactory>();
            var services = new Mock<IUsersServices>();

            login.Setup(l => l.CheckUsernameInUserDb("username")).Returns(true);

            var sut = new RegisterCommand(validator.Object, messages.Object, login.Object, factory.Object, services.Object);
            sut.Execute(parameters);

            messages.Verify(v => v.ThisUserAlreadyExistMessage(), Times.Once);
        }
        [TestMethod]
        public void Invoke_AddUserToDbMethod()
        {
            // Arrange
            IList<string> parameters = new List<string> { "username", "password" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            var factory = new Mock<IModelsFactory>();
            var services = new Mock<IUsersServices>();
            var user = new Mock<IUser>();
            user.Setup(u => u.Username).Returns("username");
            user.Setup(u => u.Password).Returns("password");
            login.Setup(l => l.CheckUsernameInUserDb("username")).Returns(false);
            login.Setup(l => l.CheckUsernameInAdminDb("username")).Returns(false);
            var newUser = factory.Setup(f => f.CreateUser("username", "password")).Returns(user.Object);
            services.Setup(s => s.AddUserToDb(user.Object));
            // Act
            var sut = new RegisterCommand(validator.Object, messages.Object, login.Object, factory.Object, services.Object);
            sut.Execute(parameters);
            // Verify
            services.Verify(v => v.AddUserToDb(user.Object), Times.Once);
        }
        [TestMethod]
        public void Invoke_ReturnCorrectMessage()
        {
            // Arrange
            IList<string> parameters = new List<string> { "username", "password" };
            var validator = new Mock<IValidator>();
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            var factory = new Mock<IModelsFactory>();
            var services = new Mock<IUsersServices>();
            var user = new Mock<IUser>();
            user.Setup(u => u.Username).Returns("username");
            user.Setup(u => u.Password).Returns("password");
            login.Setup(l => l.CheckUsernameInUserDb("username")).Returns(false);
            login.Setup(l => l.CheckUsernameInAdminDb("username")).Returns(false);
            var newUser = factory.Setup(f => f.CreateUser("username", "password")).Returns(user.Object);
            services.Setup(s => s.AddUserToDb(user.Object));
            messages.Setup(m => m.RegisterMessage("username")).Returns("Ok");
            // Act
            var sut = new RegisterCommand(validator.Object, messages.Object, login.Object, factory.Object, services.Object);
            var actual = sut.Execute(parameters);
            // Verify
            Assert.AreEqual("Ok", actual);
        }
    }
}
