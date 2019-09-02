using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Tests.UserServicesTests
{
    [TestClass]
    public class CheckIfUserExist_Should
    {
        [TestMethod]
        public void ReturnTrue_WhenUserWithPassedUsernameExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTrue_WhenUserWithPassedUsernameExist));
            var mockValidator = new Mock<IServicesValidator>();
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 1, Username = "cool" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                var result = sut.CheckIfUserExist("cool");
                Assert.AreEqual(true, result);
            }
        }
        [TestMethod]
        public void ReturnFalse_WhenUserWithPassedUsernameDoesNotExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalse_WhenUserWithPassedUsernameDoesNotExist));
            var mockValidator = new Mock<IServicesValidator>();
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 1, Username = "cool" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                var result = sut.CheckIfUserExist("foo");
                Assert.AreEqual(false, result);
            }
        }
    }
}
