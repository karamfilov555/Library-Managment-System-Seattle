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
    public class RemoveUser_Should
    {
        [TestMethod]
        public void RemoveUser_WhenValidUserPassed_AndSaveChanges()
        {
            var options = TestUtilities.GetOptions(nameof(RemoveUser_WhenValidUserPassed_AndSaveChanges));
            var mockValidator = new Mock<IServicesValidator>();
            var user = new User
            {
                Id = 1,
                Username = "cool",
                Password = "tool"
            };
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Add(user);
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                sut.RemoveUserFromDb(user);
                Assert.AreEqual(0, assertContext.Users.Count());
            }
        }
    }
}
