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
    public class ProvideRole_Should
    {
        [TestMethod]
        public void ReturnRole_ThruRoleFactory_IfRoleWithPassedName_DoesNotExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnRole_ThruRoleFactory_IfRoleWithPassedName_DoesNotExist));
            var mockRoleFactory = new Mock<IRoleFactory>();
            mockRoleFactory.Setup(f => f.CreateRole("role")).Returns(new Role { Id = 1, Name = "role" });
            using (var assertContext = new LMSContext(options))
            {
                var sut = new RoleServices(assertContext, mockRoleFactory.Object);
                var role = sut.ProvideRole("role");
                Assert.AreEqual(1, role.Id);
                Assert.IsInstanceOfType(role, typeof(Role));
            }
        }
        [TestMethod]
        public void ReturnRoleFromDb_WithPassedName_IfRoleAlreadyExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnRoleFromDb_WithPassedName_IfRoleAlreadyExist));
            var mockRoleFactory = new Mock<IRoleFactory>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Roles.Add(new Role { Id = 1, Name = "cool" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new RoleServices(assertContext, mockRoleFactory.Object);
                var role = sut.ProvideRole("cool");
                Assert.AreEqual(1, role.Id);
                Assert.AreEqual("cool", role.Name);
                Assert.IsInstanceOfType(role, typeof(Role));
            }
        }
    }
}
