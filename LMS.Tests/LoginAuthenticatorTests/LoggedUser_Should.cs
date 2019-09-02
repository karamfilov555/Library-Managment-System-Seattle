using LMS.Data;
using LMS.Models;
using LMS.Models.Models;
using LMS.Services;
using LMS.Services.Contracts;
using LMS.Services.ModelProviders.Contracts;
using LMS.Services.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.LoginAuthenticatorTests
{
    [TestClass]
    public class SetCurrentUser_Should
    {
        [TestMethod]
        public void SetCurrentUser_UsernameAndPassword()
        {
            var options = TestUtilities.GetOptions(nameof(SetCurrentUser_UsernameAndPassword));
            var mockUserServices = new Mock<IUserServices>();
            var mockValidator = new Mock<IServicesValidator>();
            var username = "cool";
            var password = "tool";
            var user = new User { Id = 1, Username = username };

            using (var arrangeContext = new LMSContext(options))
            {
                var sut = new LoginAuthenticator(mockUserServices.Object, mockValidator.Object);
                sut.SetCurrentUser(user,username,password);
                var currentUser = sut.LoggedUser();
                var currentUsername = sut.GetCurrentUserName();
                Assert.AreEqual(user, currentUser);
                Assert.AreEqual(username, currentUsername);
            }
        }
    }
}
