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
    public class CheckForMaxCheckOutAmount_Should
    {
        [TestMethod]
        public void ThrowArgumentException_IfUser_AlreadyHave5BooksInHistory()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowArgumentException_IfUser_AlreadyHave5BooksInHistory));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>();
            mockLoginAuthenticator.Setup(l => l.LoggedUser())
                .Returns(new User { Id = 3, Username = "cool" });
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "cool" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 3,
                    Title = "title",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 1,
                    Title = "titled",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 2,
                    Title = "dve",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 4,
                    Title = "chetiri",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 5,
                    Title = "pet",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 3,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 1,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 2,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 4,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 5,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator.Object, mockRecordFines, mockBookServices);
                var user = assertContext.Users.First(u => u.Id == 3);
                Assert.ThrowsException<ArgumentException>(
                    () => sut.CheckForMaxCheckOutAmount());
            }
        }
        [TestMethod]
        public void ThrowCorrectMsg_IfUser_AlreadyHave5BooksInHistory()
        {
            var options = TestUtilities.GetOptions(nameof(ThrowCorrectMsg_IfUser_AlreadyHave5BooksInHistory));
            var mockLoginAuthenticator = new Mock<ILoginAuthenticator>();
            mockLoginAuthenticator.Setup(l => l.LoggedUser())
                .Returns(new User { Id = 3, Username = "cool" });
            var mockRecordFines = new Mock<IRecordFinesServices>().Object;
            var mockBookServices = new Mock<IBookServices>().Object;

            using (var arrangeContext = new LMSContext(options))
            {
                arrangeContext.Users.Add(new User { Id = 3, Username = "cool" });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 3,
                    Title = "title",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 1,
                    Title = "titled",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 2,
                    Title = "dve",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 4,
                    Title = "chetiri",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.Books.Add(new Book
                {
                    Id = 5,
                    Title = "pet",
                    IsCheckedOut = true
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 3,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 1,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 2,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 4,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
                arrangeContext.HistoryRegistries.Add(new HistoryRegistry
                {
                    UserId = 3,
                    BookId = 5,
                    IsReturned = false
                });
                arrangeContext.SaveChanges();
            }
            using (var assertContext = new LMSContext(options))
            {
                var sut = new HistoryServices(assertContext, mockLoginAuthenticator.Object, mockRecordFines, mockBookServices);
                var user = assertContext.Users.First(u => u.Id == 3);
                var exp = Assert.ThrowsException<ArgumentException>(
                    () => sut.CheckForMaxCheckOutAmount());
                Assert.AreEqual("You have reached the maximum check-out amount of 5 books!", exp.Message);
            }
        }
    }
}
