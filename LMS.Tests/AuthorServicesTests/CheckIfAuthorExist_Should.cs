using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.ModelProviders.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Tests.AuthorServicesTests
{
    [TestClass]
    public class CheckIfAuthorExist_Should
    {
        [TestMethod]
        public void ReturnTrue_WhenAuthorNamePassed_AndDbContainsAuthor_WithThisName()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnTrue_WhenAuthorNamePassed_AndDbContainsAuthor_WithThisName));
            var mockAuthorFactory = new Mock<IAuthorFactory>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Authors.Add(new Author { Id = 1, Name = "author" });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new AuthorServices(assertContext, mockAuthorFactory.Object);
                var result = sut.CheckIfAuthorExist("author");
                Assert.AreEqual(true, result);
            }
        }
        [TestMethod]
        public void ReturnFalse_WhenAuthorNamePassed_AndDbDoesNotContainsAuthor_WithThisName()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnFalse_WhenAuthorNamePassed_AndDbDoesNotContainsAuthor_WithThisName));
            var mockAuthorFactory = new Mock<IAuthorFactory>();

            using (var assertContext = new LMSContext(options))
            {
                var sut = new AuthorServices(assertContext, mockAuthorFactory.Object);
                var result = sut.CheckIfAuthorExist("author");
                Assert.AreEqual(false, result);
            }
        }
    }
}
