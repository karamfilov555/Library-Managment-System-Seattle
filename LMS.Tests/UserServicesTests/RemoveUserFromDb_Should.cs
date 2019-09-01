using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LMS.Tests.UserServicesTests
{
    [TestClass]
    public class RemoveUserFromDb_Should
    {
        [TestMethod]
        public void RemoveUserCorrectly()
        {
            var user = new User();
            var options = TestUtilities.GetOptions(nameof(RemoveUserCorrectly));
            var mockValidator = new Mock<IServicesValidator>();

            using (var arrangeContext = new LMSContext(options))
            {
                mockValidator.Setup(v => v.CheckIfUsernameExists("go6o"));
                var sut = new UserServices(arrangeContext, mockValidator.Object);
                sut.AddUserToDb(user);
            }

            using (var assertContext = new LMSContext(options))
            {
                Assert.AreEqual(1, assertContext.Users.Count());
                assertContext.Users.Remove(user);
                assertContext.SaveChanges();
                Assert.AreEqual(0, assertContext.Users.Count());
            }
        }
    }
}
