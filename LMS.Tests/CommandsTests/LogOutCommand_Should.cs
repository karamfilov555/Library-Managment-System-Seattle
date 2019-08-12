using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using LMS.Contracts;
using LMS.Core.Commands;

namespace LMS.Tests.CommandsTests
{

    [TestClass]
    public class LogOutCommand_Should
    {
        [TestMethod]
        public void Invoke_LogOutCurrentUserMethod()
        {
            IList<string> parameters = new List<string>();
            // Arrange
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();

            // Act
            var sut = new LogoutCommand(messages.Object, login.Object);
            sut.Execute(parameters);

            // Verify
            login.Verify(m => m.LogoutCurrentUser(), Times.Once);
        }

        [TestMethod]
        public void Invoke_LogoutMessageMethod()
        {
            IList<string> parameters = new List<string>();
            // Arrange
            var messages = new Mock<IGlobalMessages>();
            var login = new Mock<ILoginAuthenticator>();
            messages.Setup(l => l.LogOutMessage()).Returns("logout");
            // Act
            var sut = new LogoutCommand(messages.Object, login.Object);
            var result = sut.Execute(parameters);

            // Assert
            Assert.AreEqual("logout", result);
        }
    }

}
