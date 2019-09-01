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
    public class AddHistory_Should
    {
        [TestMethod]
        public void AddHistoryToDb_IfHistoryRegistryDoesNotExist()
        {
            var options = TestUtilities.GetOptions(nameof(AddHistoryToDb_IfHistoryRegistryDoesNotExist));
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
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = assertContext.Users.First(u => u.Id == 3);
                var book = assertContext.Books.First(u => u.Id == 3);
                sut.AddHistoryToDb(new HistoryRegistry { User = user, Book = book });
                Assert.AreEqual(1, assertContext.HistoryRegistries.Count());
                Assert.AreEqual(user, assertContext.HistoryRegistries.First().User);
            }
        }
        [TestMethod]
        public void Change_IsReturnedStatus_IfHistoryRegistryExists()
        {
            var options = TestUtilities.GetOptions(nameof(Change_IsReturnedStatus_IfHistoryRegistryExists));
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
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator, mockRecordFines, mockBookServices);
                var user = assertContext.Users.First(u => u.Id == 3);
                var book = assertContext.Books.First(u => u.Id == 3);
                var hr = assertContext.HistoryRegistries.First();
                sut.AddHistoryToDb(hr);
                Assert.AreEqual(false, assertContext.HistoryRegistries.First().IsReturned);
            }
        }
    }
}
