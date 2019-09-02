using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.UserServicesTests
{
    [TestClass]
    public class CheckUserCredentials_Should
    {
        [TestMethod]
        public void ReturnCorrectUser_WhenValidCredentialsPassed()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnCorrectUser_WhenValidCredentialsPassed));
            var mockValidator = new Mock<IServicesValidator>();
            var user = new User
            {
                Id = 1,
                Username = "cool",
                Password = "tool"
            };
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                var result = sut.CheckUserCredetials("cool", "tool");
                Assert.AreEqual(user.Username, result.Username);
                Assert.AreEqual(user.Password, result.Password);
                Assert.IsInstanceOfType(result,typeof(User));
            }
        }
        [TestMethod]
        public void ThrowArgumentException_WhenWrongCredentialsPassed()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_WhenWrongCredentialsPassed));
            var mockValidator = new Mock<IServicesValidator>();
            var user = new User
            {
                Id = 1,
                Username = "cool",
                Password = "tool"
            };
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                Assert.ThrowsException<ArgumentException>(
                    () => sut.CheckUserCredetials("tool", "tool")); 
            }
        }
        [TestMethod]
        public void ThrowCorrectMsg_WhenWrongCredentialsPassed()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_WhenWrongCredentialsPassed));
            var mockValidator = new Mock<IServicesValidator>();
            var user = new User
            {
                Id = 1,
                Username = "cool",
                Password = "tool"
            };
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                var ex = Assert.ThrowsException<ArgumentException>(
                    () => sut.CheckUserCredetials("tool", "tool"));
                Assert.AreEqual("Wrong Credentials!", ex.Message);
            }
        }

    }
}
