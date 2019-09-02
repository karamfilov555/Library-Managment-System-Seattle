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
    public class ProvideAuthor_Should
    {
        [TestMethod]
        public void ReturnAuthor_WhenAuthorWithNamePassedExist()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnAuthor_WhenAuthorWithNamePassedExist));
            var mockAuthorFactory = new Mock<IAuthorFactory>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Authors.Add(new Author { Id = 1, Name = "author" });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new LMSContext(options))
            {
                var sut = new AuthorServices(assertContext, mockAuthorFactory.Object);
                var result = sut.ProvideAuthor("author");
                Assert.AreEqual("author", result.Name);
                Assert.IsInstanceOfType(result, typeof(Author));
            }
        }
        [TestMethod]
        public void 
            Invoke_CreateAuthorMethod_AndReturnAuthor_WhenAuthorWithNamePassedDoesNotExist()
        {
            var options = TestUtilities.GetOptions(nameof(Invoke_CreateAuthorMethod_AndReturnAuthor_WhenAuthorWithNamePassedDoesNotExist));
            var mockAuthorFactory = new Mock<IAuthorFactory>();
            mockAuthorFactory.Setup(c => c.CreateAuthor("author"))
                .Returns(new Author { Id = 1, Name = "author" });

            using (var assertContext = new LMSContext(options))
            {
                var sut = new AuthorServices(assertContext, mockAuthorFactory.Object);
                var result = sut.ProvideAuthor("author");
                Assert.AreEqual("author", result.Name);
                Assert.IsInstanceOfType(result, typeof(Author));
            }
        }
    }
}
