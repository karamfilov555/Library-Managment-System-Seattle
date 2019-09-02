using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.ModelProviders.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.RoleServicesTests
{
    [TestClass]
    public class CheckIfRoleExists_Should
    {
        [TestMethod]
        public void ReturnFalse_IfRole_WithPassedName_DoesNotExists()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalse_IfRole_WithPassedName_DoesNotExists));
            var mockRoleFactory = new Mock<IRoleFactory>();
           
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Roles.Add(new Role { Id = 1, Name = "role" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new RoleServices(assertContext, mockRoleFactory.Object);
                var result = sut.CheckIfRoleExist("other");
                Assert.AreEqual(false, result);
            }
        }
        [TestMethod]
        public void ReturnTrue_IfRole_WithPassedName_Exists()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTrue_IfRole_WithPassedName_Exists));
            var mockRoleFactory = new Mock<IRoleFactory>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Roles.Add(new Role { Id = 1, Name = "role" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new RoleServices(assertContext, mockRoleFactory.Object);
                var result = sut.CheckIfRoleExist("role");
                Assert.AreEqual(true, result);
            }
        }
    }
}
