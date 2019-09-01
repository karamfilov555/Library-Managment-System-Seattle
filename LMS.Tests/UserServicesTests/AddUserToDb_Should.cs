using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Validator;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace LMS.Tests.UserServicesTests
{
    [TestClass]
    public class AddUserToDb_Should
    {
        [TestMethod]
        public void BeSuccessfullyAdded()
        {
            var options = TestUtilities.GetOptions(nameof(BeSuccessfullyAdded));
            var mockValidator = new Mock<IServicesValidator>();

            using (var arrangeContext = new LMSContext(options))
            {
                mockValidator.Setup(v => v.CheckIfUsernameExists("go6o"));
                var sut = new UserServices(arrangeContext, mockValidator.Object);
                sut.AddUserToDb(new User());
            }

            using (var assertContext = new LMSContext(options))
            {
                Assert.AreEqual(1, assertContext.Users.Count());
            }
        }
        [TestMethod]
        public void ThrowExceptionWhenUsernameExist()
        {
            var options = TestUtilities.GetOptions(nameof(BeSuccessfullyAdded));
            var mockValidator = new Mock<IServicesValidator>();
            var user = new User() { Username = "go6o" };
            using (var arrangeContext = new LMSContext(options))
            {
               // mockValidator.Setup(v => v.CheckIfUsernameExists("go6o"));
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.AddUserToDb(user));
            }
        }
    }
}
