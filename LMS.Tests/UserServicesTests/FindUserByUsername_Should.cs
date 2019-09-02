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
    public class FindUserByUsername_Should
    {
        [TestMethod]
        public void ReturnUser_WhenUserWithPassedUsernameExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnUser_WhenUserWithPassedUsernameExist));
            var mockValidator = new Mock<IServicesValidator>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 1, Username = "cool" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                var result = sut.FindUserByUsername("cool");
                Assert.AreEqual("cool", result.Username);
            }
        }
        [TestMethod]
        public void ThrowArgumentException_WhenUserWithPassedUsernameDoesNotExists()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_WhenUserWithPassedUsernameDoesNotExists));
            var mockValidator = new Mock<IServicesValidator>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 1, Username = "cool" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                Assert.ThrowsException<ArgumentException>(
                    ()=> sut.FindUserByUsername("fool"));
            }
        }
        [TestMethod]
        public void ThrowCorrectMsg_WhenUserWithPassedUsernameDoesNotExists()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_WhenUserWithPassedUsernameDoesNotExists));
            var mockValidator = new Mock<IServicesValidator>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 1, Username = "cool" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                var ex = Assert.ThrowsException<ArgumentException>(
                    () => sut.FindUserByUsername("fool"));
                Assert.AreEqual($"User with username \"fool\" does not exist", ex.Message);
            }
        }
    }
}
