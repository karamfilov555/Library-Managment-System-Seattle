using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Validator;
using Moq;

namespace LMS.Tests.UserServicesTests
{
    [TestClass]
    public class CheckUserCredentials_Should
    {
        [TestMethod]
        public void CheckForCorrectCredential()
        {
            var options = TestUtilities.GetOptions(nameof(CheckForCorrectCredential));
            var user = new User() { Username = "go6o", Password = "123"};
            using (var arrangeContext = new LMSContext(options))
            {
                // mockValidator.Setup(v => v.CheckIfUsernameExists("go6o"));
                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new LMSContext(options))
            {
                Assert.IsTrue(assertContext.Users.Any(u=>u.Username == user.Username && u.Password == user.Password));
            }
        }
        [TestMethod]
        public void ThrowExceptionWhenUsernameAndPasswordAreNotCorrect()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowExceptionWhenUsernameAndPasswordAreNotCorrect));
            var mockValidator = new Mock<IServicesValidator>();
            //var user = new User() { Username = "go6o", Password = "123"};
            //using (var arrangeContext = new LMSContext(options))
            //{
            //    // mockValidator.Setup(v => v.CheckIfUsernameExists("go6o"));
            //    arrangeContext.Users.Add(user);
            //    arrangeContext.SaveChanges();
            //}

            using (var assertContext = new LMSContext(options))
            {
                var sut = new UserServices(assertContext, mockValidator.Object);

                Assert.ThrowsException<ArgumentException>(() => sut.CheckUserCredentials("user","password"));
            }
        }
    }
}
