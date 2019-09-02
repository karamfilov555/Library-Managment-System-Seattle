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
    public class AddRoleToDb_Should
    {
        [TestMethod]
        public void AddRoleToDb_WhenValidRolePassed_AndSaveChanges()
        {
            var options = TestUtilities.GetOptions(nameof(AddRoleToDb_WhenValidRolePassed_AndSaveChanges));
            var mockRoleFactory = new Mock<IRoleFactory>();
            using (var assertContext = new LMSContext(options))
            {
                var sut = new RoleServices(assertContext, mockRoleFactory.Object);
                sut.AddRoleToDb(new Role {Id = 1, Name = "member" });
                Assert.AreEqual(1, assertContext.Roles.Count());
            }
        }
    }
}
