using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using LMS.Models;

namespace LMS.Tests.UserTest
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void AssignPassedValues()
        {
            var sut = new User("Cool", "tool");
            Assert.IsInstanceOfType(sut, typeof(User));
        }
        [TestMethod]
        public void SetUsername_WhenCorrectValuePassed()
        {
            var sut = new User("Cool", "tool");
            Assert.AreEqual("Cool", sut.Username);
        }
        [TestMethod]
        public void SetPassword_WhenCorrectValuePassed()
        {
            var sut = new User("Cool", "tool");
            Assert.AreEqual("tool", sut.Password);
        }
        [TestMethod]
        public void ThrowArgumentException_WhenTheUsernameIsSmallerThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new User("12","password"));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenTheUsernameIsLongerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new User(new string('d',16), "password"));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenNullUsernamePassed()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new User( null, "password"));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenNullPasswordPassed()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new User("Cool", null));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenThePasswordIsSmallerThanMinValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new User("Cool", "12"));
        }
        [TestMethod]
        public void ThrowArgumentException_WhenThePasswordIsLongerThanMaxValue()
        {
            Assert.ThrowsException<ArgumentException>(
                () => new User("Cool", new string('d',16)));
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenNullUsernamePassed()
        {
            var expectedMsg = "Invalid username or password!";

            var sut = Assert.ThrowsException<ArgumentException>(
                    () => new User(null,"password"));

            Assert.AreEqual(expectedMsg, sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenNullPasswordPassed()
        {
            var expectedMsg = "Invalid username or password!";

            var sut = Assert.ThrowsException<ArgumentException>(
                    () => new User("username", null));

            Assert.AreEqual(expectedMsg, sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenPasswordShorterThanMinValuePassed()
        {
            var expectedMsg = "Invalid username or password!";

            var sut = Assert.ThrowsException<ArgumentException>(
                    () => new User("username", "12"));

            Assert.AreEqual(expectedMsg, sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenPasswordLongerThanMaxValuePassed()
        {
            var expectedMsg = "Invalid username or password!";

            var sut = Assert.ThrowsException<ArgumentException>(
                    () => new User("username", new string('d',16)));

            Assert.AreEqual(expectedMsg, sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenUsernameLongerThanMaxValuePassed()
        {
            var expectedMsg = "Invalid username or password!";

            var sut = Assert.ThrowsException<ArgumentException>(
                    () => new User(new string('d', 16),"password"));

            Assert.AreEqual(expectedMsg, sut.Message);
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMessage_WhenUsernameShorterThanMinValuePassed()
        {
            var expectedMsg = "Invalid username or password!";

            var sut = Assert.ThrowsException<ArgumentException>(
                    () => new User(new string('d', 2), "password"));

            Assert.AreEqual(expectedMsg, sut.Message);
        }
    }
}
