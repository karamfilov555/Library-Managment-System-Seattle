using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Tests.BookServicesTests
{
    [TestClass]
    public class FindBook_Should
    {
        [TestMethod]
        public void ReturnBook_WithPassedTitle_IfBookExistInDb()
        {
            var title = "title";
            var author = "author";
            var options = TestUtilities.GetOptions(nameof(ReturnBook_WithPassedTitle_IfBookExistInDb));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                var sut = new BookServices(arrangeContext, mockLoginAuthenticator);
                arrangeContext.Authors.Add(new Author { Id= 1, Name = author });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book() { Title = title, AuthorId = 1});
                arrangeContext.SaveChanges();
            }
            using (var actContext = new LMSContext(options))
            {
                var sut = new BookServices(actContext, mockLoginAuthenticator);
                var book = sut.FindBook(title,author);
            }
            using (var assertContext = new LMSContext(options))
            {
                var book = assertContext.Books
                    .FirstOrDefault(b => b.Title == title && b.AuthorId == 1);
                Assert.IsNotNull(book);
                Assert.AreEqual(title, book.Title);
                Assert.AreEqual(1, book.AuthorId);
            }
        }
        [TestMethod]
        public void ThrowArgumentException_IfThereAreNoBook_WithThisTitle()
        {
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_IfThereAreNoBook_WithThisTitle));
            
            using (var assertContext = new LMSContext(options))
            {
                var sut = new BookServices(assertContext, mockLoginAuthenticator);
                Assert.ThrowsException<ArgumentException>(
                    () => sut.FindBook("kniga", "avtor"));
            }
        }
        [TestMethod]
        public void ThrowCorrectArgumentExceptionMsg_IfThereAreNoBook_WithThisTitle()
        {
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_IfThereAreNoBook_WithThisTitle));

            using (var assertContext = new LMSContext(options))
            {
                var sut = new BookServices(assertContext, mockLoginAuthenticator);
                var exp = Assert.ThrowsException<ArgumentException>(
                     () => sut.FindBook("kniga", "avtor"));
                Assert.AreEqual(
                    "Book with title \"kniga\" and author avtor does not exist!", exp.Message);
            }
        }
    }
}
