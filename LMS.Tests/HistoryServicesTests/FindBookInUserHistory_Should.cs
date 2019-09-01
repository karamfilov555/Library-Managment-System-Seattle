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

namespace LMS.Tests.HistoryServicesTests
{
    [TestClass]
    public class FindBookInUserHistory_Should
    {
        [TestMethod]
        public void ReturnBook_IfBookExistInUserHistory_AndIsNotReturned()
        {
            var options = TestUtilities.GetOptions(nameof(ReturnBook_IfBookExistInUserHistory_AndIsNotReturned));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 3,
                    Title = "title",
                    IsCheckedOut = true
                }) ;
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 3,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var history = new HistoryServices(assertContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = assertContext.Users.First(u => u.Id == 3);
                var book = history.FindBookInUserHistory(user, "title");
                Assert.AreEqual("title", book.Title);
            }
        }
        [TestMethod]
        public void ThrowException_IfBookExistInUserHistory_ButIsReturned()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowException_IfBookExistInUserHistory_ButIsReturned));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 3,
                    Title = "title",
                    IsCheckedOut = true
                    
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 3,
                    IsReturned = true
                });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var history = new HistoryServices(assertContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = assertContext.Users.First(u => u.Id == 3);
                Assert.ThrowsException<ArgumentException>(
                    () => history.FindBookInUserHistory(user, "title"));
            }
        }
        [TestMethod]
        public void ThrowCorrectMsg_IfBookExistInUserHistory_ButIsReturned()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_IfBookExistInUserHistory_ButIsReturned));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>().Object;
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "user" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 3,
                    Title = "title",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 3,
                    IsReturned = true
                });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var history = new HistoryServices(assertContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = assertContext.Users.First(u => u.Id == 3);
                var exp = Assert.ThrowsException<ArgumentException>(
                    () => history.FindBookInUserHistory(user, "title"));
                Assert.AreEqual($"There is no book with title \"title\" in your account!", exp.Message);
            }
        }
    }
}
