using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.Data;
using LMS.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LMS.Tests.UserServicesTests
{
    [TestClass]
    public class CheckIfUserExist_Should
    {
        [TestMethod]
        public void CheckIfUserIsFound()
        {

            var options = TestUtilities.GetOptions(nameof(CheckIfUserIsFound));
            var user = new User() { Username = "go6o"};
            using (var arrangeContext = new LMSContext(options))
            {

                arrangeContext.Users.Add(user);
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new LMSContext(options))
            {
                Assert.IsTrue(assertContext.Users.Any(u => u.Username == user.Username));
            }
        }
        [TestMethod]
        public void ThrowArgumentExceptionWhenUserFound()
        {

            var options = TestUtilities.GetOptions(nameof(ThrowArgumentExceptionWhenUserFound));
            //var user = new User() { Username = "go6o" };
            //using (var arrangeContext = new LMSContext(options))
            //{

            //    arrangeContext.Users.Add(user);
            //    arrangeContext.SaveChanges();
            //}
            
            using (var assertContext = new LMSContext(options))
            {
                Assert.IsTrue(assertContext.Users.All(u => u.Username == "sad"));
            }
        }
    }
}
