using LMS.Data;
using LMS.Models;
using LMS.Services;
using LMS.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace LMS.Tests.BookServicesTests
{
    [TestClass]
    public class FindAvailableBook_Should
    {
        [TestMethod]
        public void ReturnAvailableBook()
        {
            var title = "title";
            var author = "author";
            var options = TestUtilities.GetOptions(nameof(ReturnAvailableBook));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                var sut = new BookServices(arrangeContext, mockLoginAuthenticator);
                arrangeContext.Authors.Add(new Author {Id = 1, Name = author });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book() { Title = title, AuthorId = 1});
                arrangeContext.SaveChanges();
            }
            using (var actContext = new LMSContext(options))
            {
                var sut = new BookServices(actContext, mockLoginAuthenticator);
                var book = sut.FindAvailableBook(title, author);
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
        public void ThrowArgumentException_IfBookDoesNotExist()
        {
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_IfBookDoesNotExist));

            using (var assertContext = new LMSContext(options))
            {
                var sut = new BookServices(assertContext, mockLoginAuthenticator);
                Assert.ThrowsException<ArgumentException>(
                    () => sut.FindAvailableBook("kniga", "avtor"));
            }
        }
        [TestMethod]
        public void ThrowArgumentException_IfBookExist_ButIsNotAvailable()
        {
            var title = "title";
            var author = "author";
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_IfBookExist_ButIsNotAvailable));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Authors.Add(new Author { Id = 5, Name = author });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book()
                {
                    Title = title,
                    AuthorId = 5,
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new BookServices(assertContext, mockLoginAuthenticator);

                Assert.ThrowsException<ArgumentException>(
                   () => sut.FindAvailableBook(title, author));
            }
        }

        [TestMethod]
        public void ThrowCorrectMsg_IfBookExist_ButIsNotAvailable()
        {
            var title = "title";
            var author = "author";
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_IfBookExist_ButIsNotAvailable));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Authors.Add(new Author { Id = 5, Name = author });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book() { Title = title, AuthorId = 5,
                                                                IsCheckedOut = true });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new BookServices(assertContext, mockLoginAuthenticator);

                var exp = Assert.ThrowsException<ArgumentException>(
                   () => sut.FindAvailableBook(title, author));

                Assert.AreEqual($"We are sorry at this moment all copies of a book \"{title}\" are issued. You can reserve a copy, if you want.", exp.Message);
            }
        }
        
    }
}
