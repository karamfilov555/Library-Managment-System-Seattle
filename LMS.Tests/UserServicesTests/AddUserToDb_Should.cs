using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.ModelProviders.Contracts;
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
    public class AddUserToDb_Should
    {
        [TestMethod]
        public void AddUserToDb_WhenValidUserPassed_AndSaveChanges()
        {
            var options = TestUtilities.GetOptions(nameof(AddUserToDb_WhenValidUserPassed_AndSaveChanges));
            var mockValidator = new Mock<IServicesValidator>();

            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                sut.AddUserToDb(new User { Id = 1, Username = "cool" });
                Assert.AreEqual(1, assertContext.Users.Count());
            }
        }
        [TestMethod]
        public void ThrowArgumentException_WhenUsername_AlreadyExist()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_WhenUsername_AlreadyExist));
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
                    () => sut.AddUserToDb(new User { Id = 1, Username = "cool" }));
            }
        }
    }
}
