using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.ModelProviders.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Tests.RoleServicesTests
{
    [TestClass]
    public class FindRole_Should
    {
        [TestMethod]
        public void ReturnRole_IfRoleWithPassedNameExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnRole_IfRoleWithPassedNameExist));
            var mockRoleFactory = new Mock<IRoleFactory>();
            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Add(new Role { Id = 1, Name = "test" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new RoleServices(assertContext, mockRoleFactory.Object);
                var role = sut.FindRoleInDb("test");
                Assert.AreEqual(1, role.Id);
            }
        }
    }
}
