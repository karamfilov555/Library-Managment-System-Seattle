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

namespace LMS.Tests.AuthorServicesTests
{
    [TestClass]
    public class FindAuthorByName_Should
    {
        [TestMethod]
        public void ReturnAuthor_WhenExistingAuthorsNamePassed()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnAuthor_WhenExistingAuthorsNamePassed));
            var mockAuthorFactory = new Mock<IAuthorFactory>();

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Authors.Add(new Author { Id = 1, Name = "author" });
                arrangeContext.SaveChanges();
            }

            using (var assertContext = new LMSContext(options))
            {
                var sut = new AuthorServices(assertContext, mockAuthorFactory.Object);
                var result = sut.FindAuthorByName("author");
                Assert.AreEqual("author",result.Name);
            }
        }
    }
}
