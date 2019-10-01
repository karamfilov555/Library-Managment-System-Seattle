using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Tests.BookServicesTests
{
    [TestClass]
    public class AddBook_Should
    {
        [TestMethod]
        public void Addbook()
        {
            var options = TestUtilities.GetOptions(nameof(Addbook));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            using (var actContext = new LMSContext(options))
            {
                var sut = new BookServices(actContext, mockLoginAuthenticator);
                sut.AddBookToDb(new Book());
            }
            using (var assertContext = new LMSContext(options))
            {
                var book = assertContext.Books
                    .FirstOrDefault();
                Assert.IsNotNull(book);
            }
        }
        [TestMethod]
        public void Addbook_WithCorrectValues()
        {
            var testId = 5;
            var options = TestUtilities.GetOptions(nameof(Addbook_WithCorrectValues));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            using (var actContext = new LMSContext(options))
            {
                var sut = new BookServices(actContext, mockLoginAuthenticator);
                sut.AddBookToDb(new Book() { Id = testId });
            }

            using (var assertContext = new LMSContext(options))
            {
                var book = assertContext.Books
                    .FirstOrDefault(u => u.Id == testId);
                Assert.IsNotNull(book);
                Assert.AreEqual(testId, book.Id);
            }
        }
    }
}
