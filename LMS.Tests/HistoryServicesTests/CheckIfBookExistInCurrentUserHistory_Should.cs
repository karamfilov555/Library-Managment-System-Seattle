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
    public class CheckIfBookExistInCurrentUserHistory_Should
    {
        [TestMethod]
        public void ThrowArgumentException_IfBook_IsAlreadyReturnedFromTheUser()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_IfBook_IsAlreadyReturnedFromTheUser));
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
                    Title = "title"
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
            using (var actContext = new LMSContext(options))
            {
                var history = new HistoryServices(actContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = actContext.Users.First(u => u.Id == 3);
                var book = actContext.Books.First(b => b.Id == 3);
                Assert.ThrowsException<ArgumentException>(
                    ()=>history.CheckIfBookExistInCurrentUserHistory(book, user));
            }
        }
        [TestMethod]
        public void ThrowCorrectMsg_IfBook_IsAlreadyReturnedFromTheUser()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_IfBook_IsAlreadyReturnedFromTheUser));
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
                    Title = "title"
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
            using (var actContext = new LMSContext(options))
            {
                var history = new HistoryServices(actContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = actContext.Users.First(u => u.Id == 3);
                var book = actContext.Books.First(b => b.Id == 3);
                var exp = Assert.ThrowsException<ArgumentException>(
                    () => history.CheckIfBookExistInCurrentUserHistory(book, user));
                Assert.AreEqual($"There is no book with title \"{book.Title}\" in your account!", exp.Message);
            }
        }
        
        [TestMethod]
        public void NotThrow_IfBookExistInUserHistory_AndIsNotReturned()
        {
            var options = TestUtilities.GetOptions(nameof(NotThrow_IfBookExistInUserHistory_AndIsNotReturned));
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
                    Title = "title"
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 3,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
            }
            using (var actContext = new LMSContext(options))
            {
                var history = new HistoryServices(actContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = actContext.Users.First(u => u.Id == 3);
                var book = actContext.Books.First(b => b.Id == 3);
                history.CheckIfBookExistInCurrentUserHistory(book, user);
            }
        }

    }
}
