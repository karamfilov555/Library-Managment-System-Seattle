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
    public class AddAuthorToDb_Should
    {
        [TestMethod]
        public void AddAuthorToDb_WhenValidAuthorPassed()
        {
            var options = TestUtilities.GetOptions(nameof(AddAuthorToDb_WhenValidAuthorPassed));
            var mockAuthorFactory = new Mock<IAuthorFactory>();

            using (var assertContext = new LMSContext(options))
            {
                var sut = new AuthorServices(assertContext, mockAuthorFactory.Object);
                var result = sut.AddAuthorToDb(new Author {Id = 1, Name = "author" });
                Assert.AreEqual(1, assertContext.Authors.Count());
            }
        }
    }
}
